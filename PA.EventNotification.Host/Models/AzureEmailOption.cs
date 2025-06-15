public class AzureEmailOption
{
    public const string AzureEmail = "AzureEmail";

    public string ConnectionString { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string[] ReceiverEmails { get; set; } = [];
}