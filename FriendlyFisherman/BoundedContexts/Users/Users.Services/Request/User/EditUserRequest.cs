﻿using FriendlyFisherman.SharedKernel.Services.Models;
using Users.Domain.EntityViewModels.User;

namespace Users.Services.Request
{
    using FriendlyFisherman.SharedKernel;
    using Users.Domain.EntityViewModels;

    public class EditUserRequest : ServiceRequestBase<UserViewModel>
    {
        public EditUserRequest(UserViewModel user)
        {
            User = user;
        }

        public UserViewModel User { get; set; }
    }
}
