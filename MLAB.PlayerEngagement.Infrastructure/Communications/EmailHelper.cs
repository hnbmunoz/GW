using MLAB.PlayerEngagement.Core.Models;
using System.Net;
using System.Net.Mail;

namespace MLAB.PlayerEngagement.Infrastructure.Communications;

public class EmailHelper
{
    public static string GenerateEmailBody(string userFullName, string message, string emailSignature)
    {
        return @$"<html>
                     <body>
                     <p>Hi <b>{userFullName}</b>,</p>                      
                     <p>Good day!</p>
                     <p>{message}</p>                      
                     <p>Thanks and Regards,</p>
                     <p><b>{emailSignature}</b></p>
                     </body>
                     </html>";

    }

    public static void ProcessMail(EmailRequestModel request)
    {
        MailAddressCollection recipient = new MailAddressCollection();
        MailAddressCollection cC = new MailAddressCollection();
        MailAddressCollection bCc = new MailAddressCollection();

        if (request.UserEmail != string.Empty)
        {
            foreach (string recipients in request.UserEmail.Split(';'))
                recipient.Add(recipients);
        }

        if (request.CC != string.Empty)
        {
            foreach (string emailCc in request.CC.Split(';'))
                cC.Add(emailCc);
        }

        if (request.BCC != string.Empty)
        {
            foreach (string emailBcc in request.BCC.Split(';'))
                bCc.Add(emailBcc);
        }

        SendMail(recipient, cC, bCc, request.From, string.Format(request.Subject, request.EmailType + " Password"), request.Content, request.Email, request.SmtpHost, request.Port, request.Password, request.IsSMTPWithAuth);
    }

    private static void SendMail(MailAddressCollection toAddress, MailAddressCollection cC, MailAddressCollection bCc, string from, string subject, string body, string email, string smtpHost, int port, string password, bool IsSMTPWithAuth)
    {
        MailAddress mailAddFrom = new MailAddress(email, from);
        MailMessage message = new MailMessage();
        message.From = mailAddFrom;

        foreach (MailAddress to in toAddress)
            message.To.Add(to);

        foreach (MailAddress cc in cC)
            message.CC.Add(cc);

        foreach (MailAddress bcc in bCc)
            message.Bcc.Add(bcc);

        message.Subject = subject;
        message.Body = @body;
        message.IsBodyHtml = true;

        SmtpClient client = new SmtpClient(smtpHost);

        if (IsSMTPWithAuth)
        {
            //client.Port = port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            NetworkCredential basicauthenticationinfo = new NetworkCredential(email, password);
            client.Credentials = basicauthenticationinfo;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        client.Send(message);
    }
}
