using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using Publishing.Domain.Entities.Categories;
using Publishing.Services.Request.Categories;
using Publishing.Services.Response.Categories;

namespace Publishing.Services.Abstraction.Categories
{
    public interface IThreadCategoriesService: IBaseCrudService<ThreadCategory>
    {
    }
}
