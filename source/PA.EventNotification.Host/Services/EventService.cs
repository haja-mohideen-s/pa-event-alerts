using System.Net.Http.Json;
using System.Text.Json;
using Coravel.Invocable;
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
    IHttpClientFactory httpClientFactory,
    DateSegmentFactory factory) : IInvocable
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
                var eventsList = await FetchEventsAsync(client, evt, option);
                eventResponses.AddRange(eventsList);

                foreach (var events in eventsList)
                {
                    if ((events.Data.TotalResults / 10) + 1 > events.Data.Page)
                    {
                        var nextEventList = await FetchEventsAsync(client, evt, option, events.Data.Page + 1);
                        eventResponses.AddRange(nextEventList);
                    }
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

    private async Task<IEnumerable<EventResponse>> FetchEventsAsync(HttpClient client, string evt, IOptions<OnePAOption> option, int page = 1)
    {
        List<EventResponse> responses = new();

        IDateSegment dateSegment = factory.Create();
        var requestUrls = dateSegment.GetRequestUrls(evt, page);
        foreach (string url in requestUrls)
        {
            HttpRequestMessage request = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responses.Add(await response.Content.ReadFromJsonAsync<EventResponse>() ?? throw new JsonException("Error in deserializing response"));
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
        }
        return responses;

    }

}