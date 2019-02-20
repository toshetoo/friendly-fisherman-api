using FriendlyFisherman.SharedKernel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.EntityViewModels;
using Users.Domain.Repositories;
using Users.Services.Abstraction;
using Users.Services.Request;
using Users.Services.Response;

namespace Users.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _usersRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository usersRepository, IOptions<AppSettings> appSettings)
        {
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserAuthenticationResponse> GetUserAuthenticationAsync(UserAuthenticationRequest request)
        {
            return await Task.Run(() => GetUserAuthentication(request));
        }

        private UserAuthenticationResponse GetUserAuthentication(UserAuthenticationRequest request)
        {
            var response = new UserAuthenticationResponse();

            try
            {
                var user = _usersRepository.GetByUsername(request.Username);

                if (ReferenceEquals(user, null))
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                response.AccessToken = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<GetAllUsersResponse> GetAllUsersAsync(GetAllUsersRequest request)
        {
            return await Task.Run(() => GetAllUsers(request));
        }

        private GetAllUsersResponse GetAllUsers(GetAllUsersRequest request)
        {
            var response = new GetAllUsersResponse();

            try
            {
                var users = _usersRepository.GetAllUsers();
                var usersListViewModel = new List<UserListItemViewModel>();

                foreach (var user in users)
                {
                    usersListViewModel.Add(new UserListItemViewModel
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email
                    });
                }
                response.Users = usersListViewModel;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<GetUserResponse> GetUserByIdAsync(GetUserRequest request)
        {
            return await Task.Run(() => GetUserById(request));
        }

        private GetUserResponse GetUserById(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var user = _usersRepository.GetById(request.Id);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with Id: {request.Id}");

                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                response.User = userViewModel;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<EditUserResponse> EditUserAsync(EditUserRequest request)
        {
            return await Task.Run(() => EditUser(request));
        }

        private EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();

            try
            {
                var user = _usersRepository.GetById(request.User.Id);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with Id: {request.User.Id}");

                user.Email = request.User.Email;
                user.UserName = request.User.Username;
                user.FirstName = request.User.FirstName;
                user.LastName = request.User.LastName;

                _usersRepository.Save(user);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }
    }
}
