using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.DataAccess.Repositories
{
    public class RolesRepository: BaseRepository<Role>, IRolesRepository
    {
        public RolesRepository(UsersDbContext context) : base(context)
        {
        }
    }
}
