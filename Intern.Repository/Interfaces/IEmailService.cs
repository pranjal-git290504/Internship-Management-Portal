namespace Intern.Repository.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
} 