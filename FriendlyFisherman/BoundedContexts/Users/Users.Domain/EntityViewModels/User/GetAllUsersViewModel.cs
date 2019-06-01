using System;
using System.Collections.Generic;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels.User;

namespace Users.Domain.EntityViewModels
{
    public class GetAllUsersViewModel
    {
        public List<UserListItemViewModel> Users { get; set; }
    }

    public class UserListItemViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
