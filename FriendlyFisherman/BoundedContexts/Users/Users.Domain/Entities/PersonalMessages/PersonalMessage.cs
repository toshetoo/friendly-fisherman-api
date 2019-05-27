using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Domain.Entities
{
    public class PersonalMessage
    {
        public string Id { get; set; }
        public string SenderId { get; set; }          
        public string ReceiverId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Seen { get; set; }
        public DateTime SentOn { get; set; }
        public bool DeletedBySender { get; set; }
        public bool DeletedByReceiver { get; set; }
    }
}
