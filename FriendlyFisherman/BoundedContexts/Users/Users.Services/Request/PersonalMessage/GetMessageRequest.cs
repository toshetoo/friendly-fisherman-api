﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Services.Request.PersonalMessage
{
    public class GetMessagesRequest
    {
        public string MessageId { get; set; }
        public string UserId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
