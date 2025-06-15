using Azure.Communication.Email;
using PA.EventNotification.Models;

public interface IEmailFormatter
{
    /// <summary>
    /// Formats the email body with the provided event data.
    /// </summary>
    /// <param name="evt">The event data to format.</param>
    /// <returns>A formatted email body as a string.</returns>
    Task<EmailContent> GenerateEmail(List<EventResponse> evtResponses);
}