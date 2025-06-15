using System.Text;
using Azure.Communication.Email;
using Microsoft.Extensions.Caching.Memory;
using PA.EventNotification.Models;

public class EmailFormatter(IMemoryCache cache) : IEmailFormatter
{
    /// <summary>
    /// Formats the email body with the provided event data.
    /// </summary>
    /// <param name="evtResponses">The event data to format.</param>
    /// <returns>A formatted email body as a string.</returns>
    public async Task<EmailContent> GenerateEmail(List<EventResponse> evtResponses)
    {
        try
        {
            EmailContent emailContent = new EmailContent("No events found this month")
            {
                Html = string.Empty
            };

            string emailTemplate = await LazyLoadTemplateAsync("emailTemplate", "EmailTemplates/summary.html");
            string eventTemplate = await LazyLoadTemplateAsync("eventTemplate", "EmailTemplates/event.html");

            if (evtResponses is null || !evtResponses.Any())
            {
                emailContent.Html = emailTemplate.Replace("{{content}}", "<p>No events found.</p>");
                return emailContent;
            }

            var sb = new StringBuilder();
            List<Result> results = evtResponses.SelectMany(e => e.Data.Results).ToList();
            emailContent = new($"PA Event Notification - {results.Count} events found");
            foreach (var response in results)
            {
                sb.AppendLine(eventTemplate
                    .Replace("{{title}}", response.Title)
                    .Replace("{{description}}", response.GetEventDetails())
                    .Replace("{{url}}", response.Share?.Url?.ToString() ?? response.OutletUrl)
                );

            }
            emailContent.Html = emailTemplate.Replace("{{content}}", sb.ToString());
            return emailContent;
        }
        catch (Exception ex)
        {
            throw new PAEventException("Error generating email content", ex);
        }
    }

    private async Task<string> LazyLoadTemplateAsync(string cacheKey, string templatePath)
    {
        if (!cache.TryGetValue<string>(cacheKey, out string templateContent))
        {
            templateContent = await File.ReadAllTextAsync(templatePath);
            cache.Set(cacheKey, templateContent);
        }
        return templateContent;
    }
}