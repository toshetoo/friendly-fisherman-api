using System.Collections.Generic;

namespace Users.Domain.EntityViewModels
{
    public class GetAllUsersViewModel
    {
        public List<UserListItemViewModel> Users { get; set; }
    }

    public class UserListItemViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
