using FriendlyFisherman.SharedKernel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Requests.Images;
using FriendlyFisherman.SharedKernel.Responses.Images;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using Users.Domain.EntityViewModels;
using Users.Domain.EntityViewModels.User;
using Users.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Entities;
using Users.Services.Abstraction;
using Users.Services.Request;
using Users.Services.Response;

namespace Users.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly AppSettings _appSettings;
        private readonly IImageUploaderService _imageUploaderService;

        public UserService(IUserRepository usersRepository, IOptions<AppSettings> appSettings, IUserRolesRepository userRolesRepository, IRolesRepository rolesRepository, IImageUploaderService imageUploaderService)
        {
            _usersRepository = usersRepository;
            _userRolesRepository = userRolesRepository;
            _rolesRepository = rolesRepository;
            _appSettings = appSettings.Value;
            _imageUploaderService = imageUploaderService;
        }

        public async Task<UserAuthenticationResponse> GetUserAuthenticationAsync(UserAuthenticationRequest request)
        {
            return await Task.Run(() => GetUserAuthentication(request));
        }

        /// <summary>
        /// Creates a valid JWT token for an existing user
        /// </summary>
        /// <param name="request">The user that is trying to authenticate</param>
        /// <returns>A valid JWT token if the user data is correct or NULL if no such user exists</returns>
        private UserAuthenticationResponse GetUserAuthentication(UserAuthenticationRequest request)
        {
            var response = new UserAuthenticationResponse();

            try
            {
                var user = _usersRepository.GetByUsername(request.Username);
                var userRole = _userRolesRepository.GetUserRole(user.Id);
                var role = _rolesRepository.Get(r => r.Id == userRole.RoleId);
                if (ReferenceEquals(user, null))
                    return null;

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var identity = new ClaimsIdentity(
                  new GenericIdentity(user.UserName, "TokenAuth"),
                  new[] { new Claim("ID", user.Id.ToString()),
                      new Claim(ClaimTypes.Name, user.UserName),
                      new Claim(ClaimTypes.Role, role.Name),  }
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Subject = identity,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
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

        /// <summary>
        /// Extracts all users from the DataBase
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private GetAllUsersResponse GetAllUsers(GetAllUsersRequest request)
        {
            var response = new GetAllUsersResponse();

            try
            {
                response.Items = Mapper<UserListItemViewModel, User>.MapList(_usersRepository.GetAllUsers().ToList());
                foreach (var user in response.Items)
                {
                    var userRole = _userRolesRepository.GetUserRole(user.Id);
                   user.Role = Mapper<RoleViewModel, Role>.Map(_rolesRepository.Get(r => r.Id == userRole.RoleId));
                }
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

        /// <summary>
        /// Extracts a single user from the Database.
        /// </summary>
        /// <param name="request">An object containing the id of the searched user</param>
        /// <returns>A single user with the corresponding ID or an exception with an error message</returns>
        private GetUserResponse GetUserById(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var user = _usersRepository.GetById(request.Id);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with Id: {request.Id}");

                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    string imagePath = FileHelper.BuildFilePath(_appSettings.FileUploadSettings.FilesUploadFolder, user.ImagePath);
                    user.ImagePath = FileHelper.GetImageAsBase64(imagePath);
                }

                response.Item = Mapper<UserViewModel, User>.Map(user);
                var userRole = _userRolesRepository.GetUserRole(user.Id);
                response.Item.Role = Mapper<RoleViewModel, Role>.Map(_rolesRepository.Get(r => r.Id == userRole.RoleId));
                
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<GetUserResponse> GetUserByUsernameAsync(GetUserRequest request)
        {
            return await Task.Run(() => GetUserByUsername(request));
        }

        /// <summary>
        /// Extracts a single user from the Database.
        /// </summary>
        /// <param name="request">An object containing the username of the searched user</param>
        /// <returns>A single user with the corresponding Username or an exception with an error message</returns>
        private GetUserResponse GetUserByUsername(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var user = _usersRepository.GetByUsername(request.Username);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with username: {request.Username}");

                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    string imagePath = FileHelper.BuildFilePath(_appSettings.FileUploadSettings.FilesUploadFolder, user.ImagePath);
                    user.ImagePath = FileHelper.GetImageAsBase64(imagePath);
                }

                response.Item = Mapper<UserViewModel, User>.Map(user);
                var userRole = _userRolesRepository.GetUserRole(user.Id);
                response.Item.Role = Mapper<RoleViewModel, Role>.Map(_rolesRepository.Get(r => r.Id == userRole.RoleId));

            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<GetUserResponse> AssignUserRoleAsync(GetUserRequest request)
        {
            return await Task.Run(() => AssignUserRole(request));
        }

        /// <summary>
        /// Assigns the "User" role to a newly created users
        /// </summary>
        /// <param name="request">An object containing the id of the searched user</param>
        private GetUserResponse AssignUserRole(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var roleUser = _rolesRepository.Get(r => r.Name == "User");
                _userRolesRepository.Create(new UserRole()
                {
                    UserId = request.Id,
                    RoleId = roleUser.Id
                });

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

        /// <summary>
        /// Modifies an existing user or creates a new one
        /// </summary>
        /// <param name="request">
        /// The user information that should be handled. If an ID exist in
        /// this object, the user will be modified otherwise a new user will be created
        /// </param>
        /// <returns>An exception if the user data is invalid</returns>
        private EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();

            try
            {
                var user = _usersRepository.GetById(request.User.Id);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with Id: {request.User.Id}");

                user.Email = request.User.Email;
                user.UserName = request.User.UserName;
                user.FirstName = request.User.FirstName;
                user.LastName = request.User.LastName;
                user.ImagePath = request.User.ImagePath;

                _usersRepository.Save(user);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<GetUserResponse> GetUserByEmailAsync(GetUserRequest request)
        {
            return await Task.Run(() => GetUserByEmail(request));
        }

        /// <summary>
        /// Extracts a single user from the Database.
        /// </summary>
        /// <param name="request">An object containing the email of the searched user</param>
        /// <returns>A single user with the corresponding Email or an exception with an error message</returns>
        private GetUserResponse GetUserByEmail(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var user = _usersRepository.GetByEmail(request.Email);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with email: {request.Email}");

                if (!string.IsNullOrEmpty(user.ImagePath))
                {
                    string imagePath = FileHelper.BuildFilePath(_appSettings.FileUploadSettings.FilesUploadFolder, user.ImagePath);
                    user.ImagePath = FileHelper.GetImageAsBase64(imagePath);
                }

                response.Item = Mapper<UserViewModel, User>.Map(user);
                var userRole = _userRolesRepository.GetUserRole(user.Id);
                response.Item.Role = Mapper<RoleViewModel, Role>.Map(_rolesRepository.Get(r => r.Id == userRole.RoleId));
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public async Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request)
        {
            return await Task.Run(() => UploadImage(request));
        }

        private UploadImageResponse UploadImage(UploadImageRequest request)
        {
            var response = new UploadImageResponse();

            try
            {
                var user = _usersRepository.GetById(request.Id);
                if (ReferenceEquals(user, null))
                    throw new Exception($"There is no user with Id: {request.Id}");

                var imageResp = _imageUploaderService.UploadImageAsync(request).Result;
                user.ImagePath = $"{imageResp.Item.ImageSource}";
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
