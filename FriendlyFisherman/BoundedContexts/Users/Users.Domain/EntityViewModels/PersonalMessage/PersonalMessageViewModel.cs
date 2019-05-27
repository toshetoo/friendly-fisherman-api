using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Users.Domain.EntityViewModels.PersonalMessage
{
    public class PersonalMessageViewModel
    {
        public string Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Seen { get; set; }
        [Required]
        public DateTime SentOn { get; set; }

        public bool DeletedByReceiver { get; set; }
        public bool DeletedBySender { get; set; }

        public PersonalMessageViewModel()
        {
            
        }

        public PersonalMessageViewModel(string id, string senderId, string receiverId, string title, string content, bool seen, DateTime sentOn)
        {
            Id = id;
            SenderId = senderId;
            ReceiverId = receiverId;
            Title = title;
            Content = content;
            Seen = seen;
            SentOn = sentOn;
        }

        public PersonalMessageViewModel(Entities.PersonalMessage message)
        {
            Id = message.Id;
            SenderId = message.SenderId;
            ReceiverId = message.ReceiverId;
            Title = message.Title;
            Content = message.Content;
            Seen = message.Seen;
            SentOn = message.SentOn;
            DeletedByReceiver = message.DeletedByReceiver;
            DeletedBySender = message.DeletedBySender;
        }
    }
}
