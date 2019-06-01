using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;
using System.Web.Helpers;

namespace Users.DataAccess
{
    public class UsersDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<PersonalMessage> UserPersonalMessages { get; set; }
        public UsersDbContext()
        {

        }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roleId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<UserRole>().ToTable("UserRoles").HasData(new UserRole()
            {
                RoleId = roleId,
                UserId = userId
            });
            
            modelBuilder.Entity<UserToken>().ToTable("UserTokens").HasKey(ur => new { ur.LoginProvider, ur.UserId, ur.Name });
            modelBuilder.Entity<User>().ToTable("Users").HasData(new User()
                {
                    Id = userId,
                    Email = "admin@ff.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Adminov",
                    UserName = "admin",
                    AccessFailedCount = 0,
                    NormalizedEmail = "ADMIN@FF.COM",
                    NormalizedUserName = "ADMIN",
                    PhoneNumber = "99999999",
                    PhoneNumberConfirmed = true,
                    PasswordHash = Crypto.HashPassword("Qwerty123!")
                }
            );
            modelBuilder.Entity<Role>().ToTable("Roles").HasData(new Role()
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims");
            // modelBuilder.Entity<PersonalMessage>().ToTable("UserPersonalMessages");
        }
    }
}
