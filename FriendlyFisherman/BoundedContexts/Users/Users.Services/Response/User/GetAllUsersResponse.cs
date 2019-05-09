using FriendlyFisherman.SharedKernel.Services.Models;

namespace Users.Services.Response
{
    using FriendlyFisherman.SharedKernel;
    using System.Collections.Generic;
    using Users.Domain.EntityViewModels;

    public class GetAllUsersResponse : ServiceResponseBase<UserListItemViewModel>
    {
    }
}
