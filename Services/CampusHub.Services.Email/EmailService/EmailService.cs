using System.Net;
using System.Net.Mail;

namespace CampusHub.Services.Email;

public class EmailService : IEmailService
{
    private readonly SMTPSettings _smtpSettings;

    public EmailService(SMTPSettings smtpSettings)
    {
        _smtpSettings = smtpSettings;
    }

    public async void SendEmail(string toEmail, string subject, string content)
    {
        var smtpServer = _smtpSettings.Host;
        var smtpPort = _smtpSettings.Port;
        var smtpUsername = _smtpSettings.Username;
        var smtpPassword = _smtpSettings.Password;

        var from = new MailAddress(smtpUsername, "Auctionify");
        var to = new MailAddress(toEmail);
        var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = content,
            IsBodyHtml = true
        };

        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;
            await client.SendMailAsync(message);
        }
    }
}
