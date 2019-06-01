using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.DataAccess.Repositories
{
    public class UserRolesRepository: BaseRepository<UserRole>, IUserRolesRepository
    {
        public UserRolesRepository(UsersDbContext context) : base(context)
        {
        }

        public UserRole GetUserRole(string userId)
        {
            return base.Get(r => r.UserId == userId);
        }
    }
}
