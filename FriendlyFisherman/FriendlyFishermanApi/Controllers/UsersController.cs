using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels;
using Users.Services.Abstraction;
using Users.Services.Request;

namespace FriendlyFishermanApi.Controllers
{
    [Authorize("Bearer")]
    public class UsersController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var request = new GetAllUsersRequest();
            var response = await _userService.GetAllUsersAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }
            else
            {
                _logger.LogError(response.Exception.Message);
                return Ok(response.Exception);
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var request = new GetUserRequest(id);
            var response = await _userService.GetUserByIdAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }
            else
            {
                _logger.LogError(response.Exception.Message);
                return Ok(response.Exception);
            }
        }

        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            var request = new EditUserRequest(model);
            var response = await _userService.EditUserAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }
            else
            {
                _logger.LogError(response.Exception.Message);
                return Ok(response.Exception);
            }
        }
    }
}