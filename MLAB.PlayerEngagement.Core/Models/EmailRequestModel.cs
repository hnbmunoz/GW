namespace MLAB.PlayerEngagement.Core.Models;

public class EmailRequestModel
{
    public string Content { get; set; }
    public string UserEmail { get; set; }
    public string EmailType { get; set; }
    public string Subject { get; set; }
    public string From { get; set; }
    public string CC { get; set; }
    public string BCC { get; set; }
    public bool IsSMTPWithAuth { get; set; }
    public string Email { get; set; }
    public string SmtpHost { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}
