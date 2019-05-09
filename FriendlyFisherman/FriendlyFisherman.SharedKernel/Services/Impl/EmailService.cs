using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Models.EmailTemplates;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Settings;

namespace FriendlyFisherman.SharedKernel.Services.Impl
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(EmailTemplateModel model, EmailSettings settings, string emailTo, string templateName, string subject)
        {
            var from = new MailAddress(settings.EmailAccountName);
            var to = new MailAddress(emailTo);

            var message = new MailMessage(from, to)
            {
                IsBodyHtml = true,
                Subject = subject
            };

            var body = ReplaceText(model, FileHelper.LoadTemplate(settings.EmailsTemplatesFolder, templateName));
            message.Body = body;

            CreateMailClient(settings).SendMailAsync(message);
        }

        private SmtpClient CreateMailClient(EmailSettings settings)
        {
            var client = new SmtpClient
            {
                Host = settings.Host,
                Port = settings.Port,
                UseDefaultCredentials = settings.UseDefaultCredentials,
                EnableSsl = settings.EnableSsl,
                Credentials = new NetworkCredential(settings.EmailAccount, settings.EmailPassword)
            };

            return client;
        }

        private string ReplaceText(EmailTemplateModel model, string body)
        {
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                var propertyName = "@[" + propertyInfo.Name + "]";
                var propertyValue = propertyInfo.GetValue(model, null)?.ToString();

                body = body.Replace(propertyName, propertyValue);
            }

            return body;
        }
    }

}
