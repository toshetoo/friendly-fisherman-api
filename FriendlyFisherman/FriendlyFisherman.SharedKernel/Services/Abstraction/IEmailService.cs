using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Models.EmailTemplates;
using FriendlyFisherman.SharedKernel.Settings;

namespace FriendlyFisherman.SharedKernel.Services.Abstraction
{
    public interface IEmailService
    {
        Task SendAsync(EmailTemplateModel model, EmailSettings settings, string emailTo, string templateName, string subject);
    }

}
