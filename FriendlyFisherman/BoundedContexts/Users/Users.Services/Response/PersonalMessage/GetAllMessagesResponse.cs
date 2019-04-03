using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Users.Domain.EntityViewModels.PersonalMessage;

namespace Users.Services.Response.PersonalMessage
{
    public class GetAllMessagesResponse: ServiceResponseBase
    {
        public IEnumerable<PersonalMessageViewModel> Messages { get; set; }
    }
}
