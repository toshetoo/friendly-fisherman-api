using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IRolesRepository: IBaseRepository<Role>
    {
        
    }
}
