using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class SeenCountRepository : RepositoryBase<SeenCount>, ISeenCountRepository
    {
        public SeenCountRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve a SeenCount with specific ID
        /// </summary>
        /// <param name="id">The ID of the category</param>
        /// <returns>A single SeenCount</returns>
        public SeenCount GetById(string id)
        {
            var repo = CreateRepo();
            return repo.Get(c => c.Id == id);
        }

        /// <summary>
        /// Get all available SeenCounts
        /// </summary>
        /// <returns>A list of SeenCounts or an empty list if none exist</returns>
        public IEnumerable<SeenCount> GetAll()
        {
            var repo = CreateRepo();
            return repo.GetAll();
        }

        /// <summary>
        /// Saves or updates a SeenCount. If the SeenCount object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new SeenCount will be created
        /// </summary>
        /// <param name="m">A SeenCount that should be modified or created</param>
        public void Save(SeenCount c)
        {
            var repo = CreateRepo();

            if (c.Id == null)
            {
                repo.Create(c);
            }
            else
            {
                repo.Update(c);
            }
        }

        /// <summary>
        /// Deletes a SeenCount from the DB
        /// </summary>
        /// <param name="id">The ID of the SeenCount that should be deleted</param>
        public void Delete(string id)
        {
            var repo = CreateRepo();
            repo.Delete(c => c.Id == id);
        }

    }
}
