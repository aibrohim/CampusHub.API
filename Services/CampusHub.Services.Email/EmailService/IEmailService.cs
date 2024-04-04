namespace CampusHub.Services.Email;

public interface IEmailService
{
    public void SendEmail(string to, string subject, string content);
}