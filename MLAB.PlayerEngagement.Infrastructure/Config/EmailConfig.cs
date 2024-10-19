namespace MLAB.PlayerEngagement.Infrastructure.Config;

public class EmailConfig
{
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    public string SmtpHost { get; set; }
    public string Port { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
    public string Recipient { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string EmailSignature { get; set; }
    public string IsSMTPWithAuth { get; set; }
    public string TicketManagementSubject { get; set; }
    public string TicketManagementAutoActionSubject { get;set; }
}
