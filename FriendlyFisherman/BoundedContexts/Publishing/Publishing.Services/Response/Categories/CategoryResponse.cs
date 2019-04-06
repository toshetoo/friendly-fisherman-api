using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Publishing.Domain.EntityViewModels.Categories;

namespace Publishing.Services.Response.Categories
{
    public class CategoryResponse: ServiceResponseBase<ThreadCategoryViewModel>
    {
        public ThreadCategoryViewModel ThreadCategory { get; set; }
    }
}
