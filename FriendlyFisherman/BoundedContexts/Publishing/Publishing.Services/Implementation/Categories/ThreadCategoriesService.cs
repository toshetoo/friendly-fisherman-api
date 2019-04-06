using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Impl;
using Publishing.DataAccess.Repositories.Categories;
using Publishing.Domain.Entities.Categories;
using Publishing.Services.Abstraction.Categories;
using Publishing.Services.Request.Categories;
using Publishing.Services.Response.Categories;

namespace Publishing.Services.Implementation.Categories
{
    public class ThreadCategoriesService: BaseCrudService<ThreadCategory, ThreadCategoriesRepository>, IThreadCategoriesService
    {
        public ThreadCategoriesService(ThreadCategoriesRepository repo) : base(repo)
        {
        }
    }
}
