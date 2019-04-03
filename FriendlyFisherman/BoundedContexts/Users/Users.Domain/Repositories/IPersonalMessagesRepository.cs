using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IPersonalMessagesRepository
    {
        IEnumerable<PersonalMessage> GetAllMessagesBySenderId(string id);
        IEnumerable<PersonalMessage> GetAllMessagesByReceiverId(string id);
        PersonalMessage GetMessageById(string id);
        void SaveMessage(PersonalMessage m);
        void DeleteMessage(string id);
    }
}
