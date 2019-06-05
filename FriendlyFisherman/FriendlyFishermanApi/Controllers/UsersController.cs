using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel.Requests.Images;
using FriendlyFisherman.SharedKernel.Services.Models;
using FriendlyFisherman.SharedKernel.ViewModels;
using Users.Domain.EntityViewModels;
using Users.Domain.EntityViewModels.User;
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
            var response = await _userService.GetAllUsersAsync(new GetAllUsersRequest());

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var request = new GetUserRequest { Id = id };
            var response = await _userService.GetUserByIdAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpGet]
        [Route("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var request = new GetUserRequest { Username = username };
            var response = await _userService.GetUserByUsernameAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
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

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(ImageUploadViewModel model)
        {
            var request = new UploadImageRequest(model);
            var response = await _userService.UploadImageAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));
        }
    }
}