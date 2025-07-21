using Intern.Models;
using Intern.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Intern.Repository
{
    public class EmailService : IEmailService
    {
        private readonly AppSetting _appSetting;

        public EmailService(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var fromAddress = new MailAddress(_appSetting.EmailConfig.From);
            var toAddress = new MailAddress(to);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _appSetting.EmailConfig.Key)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
} 