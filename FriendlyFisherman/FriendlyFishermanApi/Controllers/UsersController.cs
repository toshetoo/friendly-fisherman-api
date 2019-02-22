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
    //[Authorize]
    public class UsersController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] RegisterUserViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return Ok(new { error = "Unavaible username and password" });
            }

            var request = new UserAuthenticationRequest(model.Username);
            var response = await _userService.GetUserAuthenticationAsync(request);

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
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Ok(new { error = "Was not able to create a user." });
            }

            var request = new UserAuthenticationRequest(model.Username);
            var response = _userService.GetUserAuthenticationAsync(request);

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