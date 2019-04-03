using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Services.Request.PersonalMessage
{
    public class GetAllMessagesRequest
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
