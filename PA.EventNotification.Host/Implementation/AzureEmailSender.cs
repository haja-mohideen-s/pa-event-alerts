using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class AzureEmailSender(IOptions<AzureEmailOption> emailOption, ILogger<AzureEmailSender> logger) : IEmailSender
{

    public async Task SendEmailAsync(EmailContent email)
    {
        try
        {
            var emailClient = new EmailClient(emailOption.Value.ConnectionString);
            var recipients = emailOption.Value.ReceiverEmails.Select(email => new EmailAddress(email)).ToList();

            var emailMessage = new EmailMessage(
            senderAddress: emailOption.Value.SenderEmail,
            content: email,
            recipients: new EmailRecipients(recipients));

            EmailSendOperation emailSendOperation = await emailClient.SendAsync(Azure.WaitUntil.Started, emailMessage);
            logger.LogInformation("Email queued successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email using Azure Email Service.");
        }

    }
}