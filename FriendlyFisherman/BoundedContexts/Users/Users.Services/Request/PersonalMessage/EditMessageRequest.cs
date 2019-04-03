using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.EntityViewModels.PersonalMessage;

namespace Users.Services.Request.PersonalMessage
{
    public class EditMessageRequest
    {
        public PersonalMessageViewModel Message { get; set; }

        public EditMessageRequest(PersonalMessageViewModel message)
        {
            Message = message;
        }
    }
}
