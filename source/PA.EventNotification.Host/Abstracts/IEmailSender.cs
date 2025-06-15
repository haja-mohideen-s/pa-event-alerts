using Azure.Communication.Email;

public interface IEmailSender
{
    Task SendEmailAsync(EmailContent email);
}