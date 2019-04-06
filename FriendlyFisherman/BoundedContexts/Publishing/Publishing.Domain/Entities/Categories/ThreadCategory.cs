using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Publishing.Domain.Entities.Categories
{
    public class ThreadCategory: BaseEntity
    {
        public string Name { get; set; }
    }
}
