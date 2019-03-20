using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
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
            MailAddress from = new MailAddress(settings.EmailAccount);
            MailAddress to = new MailAddress(emailTo);

            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;
            message.Subject = subject;

            string body = ReplaceText(model, FileHelper.LoadTemplate(settings.EmailsTemplatesFolder, templateName));
            message.Body = body;

            CreateMailClient(settings).SendMailAsync(message);
        }

        private SmtpClient CreateMailClient(EmailSettings settings)
        {
            SmtpClient client = new SmtpClient();
            client.Host = settings.Host;
            client.Port = settings.Port;
            client.UseDefaultCredentials = settings.UseDefaultCredentials;
            client.EnableSsl = settings.EnableSsl;
            client.Credentials = new NetworkCredential(settings.EmailAccount, settings.EmailPassword);

            return client;
        }

        private string ReplaceText(EmailTemplateModel model, string body)
        {
            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                string propertyName = "@[" + propertyInfo.Name + "]";
                string propertyValue = propertyInfo.GetValue(model, null)?.ToString();

                body = body.Replace(propertyName, propertyValue);
            }

            return body;
        }
    }

}
