using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.Repositories.Categories;

namespace Publishing.DataAccess.Repositories.Categories
{
    public class ThreadCategoriesRepository: RepositoryBase<ThreadCategory>, IThreadCategoriesRepository
    {

        public ThreadCategoriesRepository(PublishingDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieve a category with specific ID
        /// </summary>
        /// <param name="id">The ID of the category</param>
        /// <returns>A single message</returns>
        public ThreadCategory GetById(string id)
        {
            var repo = CreateRepo();
            return repo.Get(c => c.Id == id);
        }

        /// <summary>
        /// Get all available categories
        /// </summary>
        /// <returns>A list of categories or an empty list if none exist</returns>
        public IEnumerable<ThreadCategory> GetAll()
        {
            var repo = CreateRepo();
            return repo.GetAll();
        }

        /// <summary>
        /// Saves or updates a category. If the ThreadCategory object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new ThreadCategory will be created
        /// </summary>
        /// <param name="m">A ThreadCategory that should be modified or created</param>
        public void Save(ThreadCategory c)
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
        /// Deletes a ThreadCategory from the DB
        /// </summary>
        /// <param name="id">The ID of the ThreadCategory that should be deleted</param>
        public void Delete(string id)
        {
            var repo = CreateRepo();
            repo.Delete(c => c.Id == id);
        }
    }
}
