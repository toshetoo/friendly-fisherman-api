using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IPersonalMessagesRepository
    {
        int GetNewMessagesCount(string id);
        IEnumerable<PersonalMessage> GetAllMessagesBySenderId(string id);
        IEnumerable<PersonalMessage> GetAllMessagesByReceiverId(string id);
        IEnumerable<PersonalMessage> GetAllMessagesBySenderIdAndReceiverId(string senderId, string receiverId);
        PersonalMessage GetMessageById(string id);
        void SaveMessage(PersonalMessage m);
        void DeleteMessage(string id);
    }
}
