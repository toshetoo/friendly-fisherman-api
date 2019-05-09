using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.Repositories.Categories;

namespace Publishing.DataAccess.Repositories.Categories
{
    public class ThreadCategoriesRepository: BaseRepository<ThreadCategory>, IThreadCategoriesRepository
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
            return base.Get(c => c.Id == id);
        }

        /// <summary>
        /// Get all available categories
        /// </summary>
        /// <returns>A list of categories or an empty list if none exist</returns>
        public new IEnumerable<ThreadCategory> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Saves or updates a category. If the ThreadCategory object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new ThreadCategory will be created
        /// </summary>
        /// <param name="m">A ThreadCategory that should be modified or created</param>
        public void Save(ThreadCategory c)
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
        /// Deletes a ThreadCategory from the DB
        /// </summary>
        /// <param name="id">The ID of the ThreadCategory that should be deleted</param>
        public void Delete(string id)
        {
            base.Delete(c => c.Id == id);
        }
    }
}
