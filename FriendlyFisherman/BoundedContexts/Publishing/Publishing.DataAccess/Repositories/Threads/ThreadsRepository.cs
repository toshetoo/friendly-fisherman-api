using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class ThreadsRepository: BaseRepository<Thread>, IThreadsRepository
    {
        public ThreadsRepository(PublishingDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve a thread with specific ID
        /// </summary>
        /// <param name="id">The ID of the category</param>
        /// <returns>A single thread</returns>
        public Thread GetById(string id)
        {
            return base.Get(c => c.Id == id);
        }

        /// <summary>
        /// Get all available threads
        /// </summary>
        /// <returns>A list of threads or an empty list if none exist</returns>
        public new IEnumerable<Thread> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Saves or updates a thread. If the Thread object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new Thread will be created
        /// </summary>
        /// <param name="m">A Thread that should be modified or created</param>
        public void Save(Thread c)
        {
            if (c.Id == null)
            {
                base.Create(c);
            }
            else
            {
                base.Update(c);
            }
        }

        /// <summary>
        /// Deletes a Thread from the DB
        /// </summary>
        /// <param name="id">The ID of the Thread that should be deleted</param>
        public void Delete(string id)
        {
            base.Delete(c => c.Id == id);
        }
    }
}
