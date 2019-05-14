using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Services.Response.PersonalMessage
{
    public class GetNewMessagesCountResponse
    {
        public int NumberOfNewMessages { get; set; }
        public Exception Exception { get; set; }
    }
}
