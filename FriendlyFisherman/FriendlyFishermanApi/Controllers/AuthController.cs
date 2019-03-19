using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels;
using Users.Services.Abstraction;
using Users.Services.Request;

namespace FriendlyFishermanApi.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {

        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return Ok(new { error = "Unavailable username or password" });
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
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
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
    }
}
