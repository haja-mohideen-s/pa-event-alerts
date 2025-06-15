using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Coravel.Invocable;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PA.EventNotification.Models;

namespace PA.EventNotification;

public class EventService(
    ILogger<EventService> logger,
    IOptions<OnePAOption> option,
    EventType eventType,
    IEmailSender emailSender,
    IEmailFormatter emailFormatter,
    IHttpClientFactory httpClientFactory) : IInvocable
{
    public async Task Invoke()
    {
        try
        {
            logger.LogInformation("EventService is starting.");

            List<EventResponse> eventResponses = new();

            HttpClient client = httpClientFactory.CreateClient(OnePAOption.OnePA);

            foreach (var evt in eventType.Events)
            {
                var eventList = await FetchEventsAsync(client, evt, option);
                eventResponses.Add(eventList);

                if ((eventList.Data.TotalResults / 10) + 1 > eventList.Data.Page)
                {
                    var nextEventList = await FetchEventsAsync(client, evt, option, eventList.Data.Page + 1);
                    eventResponses.Add(nextEventList);
                }
            }
            await emailSender.SendEmailAsync(await emailFormatter.GenerateEmail(eventResponses));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing events.");
        }
        finally
        {
            logger.LogInformation("EventService has completed its execution.");
        }
    }

    private async Task<EventResponse> FetchEventsAsync(HttpClient client, string evt, IOptions<OnePAOption> option, int page = 1)
    {
        EventResponse eventList = new();
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(client.GetEventUri(evt, option, page))
        };

        try
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            eventList = await response.Content.ReadFromJsonAsync<EventResponse>() ?? throw new JsonException("Error in deserializing response");
        }
        catch (JsonException je)
        {
            logger.LogError(je, "Error in deserializing response for {evt}", evt);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching events for {evt} with query: {RequestUri}", evt, request.RequestUri);
            throw;
        }
        return eventList;
    }

}