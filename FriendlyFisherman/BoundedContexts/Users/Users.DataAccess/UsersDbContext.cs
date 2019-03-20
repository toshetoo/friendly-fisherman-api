using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Users.Domain.Entities;

namespace Users.DataAccess
{
    public class UsersDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public UsersDbContext()
        {

        }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            modelBuilder.Entity<UserRole>().ToTable("UserRoles").HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<UserToken>().ToTable("UserTokens").HasKey(ur => new { ur.LoginProvider, ur.UserId, ur.Name });
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
        }
    }
}
