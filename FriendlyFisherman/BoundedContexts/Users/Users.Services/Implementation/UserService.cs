using FriendlyFisherman.SharedKernel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public UserAuthenticationResponse GetAuth(UserAuthenticationRequest request)
        {
            var response = new UserAuthenticationResponse();

            try
            {
                var user = _usersRepository.GetByUsername(request.Username);

                if (ReferenceEquals(user, null))
                    return null;

                // authentication successful so generate jwt token
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

        public GetAllUsersResponse GetAllUsersAsync(GetAllUsersRequest request)
        {
            var response = new GetAllUsersResponse();

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

            return response;
        }
    }
}
