using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.DataAccess.Repositories
{
    public class PersonalMessagesRepository: RepositoryBase<PersonalMessage>, IPersonalMessagesRepository
    {
        public PersonalMessagesRepository(UsersDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve all messages from the DB for a specific sender
        /// </summary>
        /// <param name="id">The ID of the sender</param>
        /// <returns>A list of messages</returns>
        public IEnumerable<PersonalMessage> GetAllMessagesBySenderId(string id)
        {
            var repo = CreateRepo();
            return repo.GetWhere(m => m.SenderId == id);
        }

        /// <summary>
        /// Retrieve all messages from the DB for a specific sender and a specific receiver
        /// </summary>
        /// <param name="id">The ID of the sender</param>
        /// <returns>A list of messages</returns>
        public IEnumerable<PersonalMessage> GetAllMessagesBySenderIdAndReceiverId(string senderId, string receiverId)
        {
            var repo = CreateRepo();
            return repo.GetWhere(m => m.SenderId == senderId && m.ReceiverId == receiverId);
        }

        /// <summary>
        /// Retrieve all messages from the DB for a specific receiver
        /// </summary>
        /// <param name="id">The ID of the receiver</param>
        /// <returns>A list of messages</returns>
        public IEnumerable<PersonalMessage> GetAllMessagesByReceiverId(string id)
        {
            var repo = CreateRepo();
            return repo.GetWhere(m => m.ReceiverId == id);
        }

        /// <summary>
        /// Retrieve a message with specific ID
        /// </summary>
        /// <param name="id">The ID of the message</param>
        /// <returns>A single message</returns>
        public PersonalMessage GetMessageById(string id)
        {
            var repo = CreateRepo();
            return repo.Get(m => m.Id == id);
        }

        /// <summary>
        /// Saves or updates a message. If the PersonalMessage object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new message will be created
        /// </summary>
        /// <param name="m">A message that should be modified or created</param>
        public void SaveMessage(PersonalMessage m)
        {
            var repo = CreateRepo();

            if (m.Id == null)
            {
                repo.Create(m);
            }
            else
            {
                repo.Update(m);
            }
        }

        /// <summary>
        /// Deletes a message from the DB
        /// </summary>
        /// <param name="id">The ID of the message that should be deleted</param>
        public void DeleteMessage(string id)
        {
            var repo = CreateRepo();
            repo.Delete(m => m.Id == id);
        }
    }
}
