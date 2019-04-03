using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Users.Domain.EntityViewModels.PersonalMessage;

namespace Users.Services.Response.PersonalMessage
{
    public class GetMessageResponse: ServiceResponseBase
    {
        public PersonalMessageViewModel Message { get; set; }
    }
}
