using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Models.EmailTemplates;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
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
        private readonly IEmailService _emailService;
        private AppSettings _settings;

        public AuthController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UsersController> logger, IEmailService emailService, AppSettings settings)
        {
            _logger = logger;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _settings = settings;
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
                Email = model.Email,
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

        [HttpPost]
        [Route("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(ResetPasswordViewModel model)
        {
            var request = new GetUserRequest(model.Email);
            var response = await _userService.GetUserByEmailAsync(request);
            var user = new User
            {
                UserName = response.User.Username,
                Email = response.User.Email,
                FirstName = response.User.FirstName,
                LastName = response.User.LastName
            };

            if (response.User == null)
            {
                // add error
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = $"{_settings.EmailSettings.SiteRedirectUrl}/reset-password?token={token}";
            EmailTemplateModel emailModel = EmailTemplateModel.Create(user.FirstName, callBackUrl);

            _emailService.SendAsync(emailModel, _settings.EmailSettings, user.Email, _settings.EmailSettings.ResetPasswordEmailTemplate, _settings.EmailSettings.ResetPasswordSubject);


            return Ok(new { token });
        }

        [HttpPost]
        [Route("SetNewPassword")]
        public async Task<IActionResult> SetNewPassword(ResetPasswordViewModel model)
        {
            var request = new GetUserRequest(model.Email);
            var response = await _userService.GetUserByEmailAsync(request);
            var user = new User
            {
                UserName = response.User.Username,
                Email = response.User.Email,
                FirstName = response.User.FirstName,
                LastName = response.User.LastName
            };

            if (response.User == null)
            {
                // add error
                return NotFound();
            } 

            await _userManager.ResetPasswordAsync(user, model.PasswordToken, model.Password);

            return Ok();
        }
    }
}
