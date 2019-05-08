using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Models.EmailTemplates;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Web;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Services.Models;
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
        private readonly AppSettings _settings;

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
            var user = await _userManager.FindByNameAsync(model.Username);
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest(new ErrorResponse(ErrorMessages.EmailNotConfirmed));
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return BadRequest(new ErrorResponse(ErrorMessages.IncorrectUsernameOrPassword));
            }

            var request = new UserAuthenticationRequest(model.Username);
            var response = await _userService.GetUserAuthenticationAsync(request);

            if (ReferenceEquals(response.Exception, null))
            {
                return Ok(response);
            }

            _logger.LogError(response.Exception, response.Exception.Message);
            return StatusCode(500, new ErrorResponse(response.Exception.Message));

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
                LastName = model.LastName,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Ok(new ErrorResponse(ErrorMessages.CouldNotCreateUser));
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callBackUrl = $"{_settings.EmailSettings.SiteRedirectUrl}/confirm?id={user.Id}&token={HttpUtility.UrlEncode(token)}";
            var emailTemplateModel = EmailTemplateModel.Create(user.FirstName, callBackUrl);

            _emailService.SendAsync(emailTemplateModel, _settings.EmailSettings, user.Email, _settings.EmailSettings.AccountConfirmationEmailTemplate, _settings.EmailSettings.AccountConfirmationSubject);

            
            return NoContent();
        }


        [HttpPost]
        [Route("ConfirmAccount")]
        public async Task<ActionResult> ConfirmEmail(ConfirmAccountViewModel model)
        {
            if (model.Id == null || model.Token == null)
            {
                return BadRequest(new ErrorResponse(ErrorMessages.RequiredFields));
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPost]
        [Route("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound(new ErrorResponse(ErrorMessages.MissingUserEmail));
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new ErrorResponse(ErrorMessages.EmailNotConfirmed));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = $"{_settings.EmailSettings.SiteRedirectUrl}/reset-password?token={token}";
            var emailModel = EmailTemplateModel.Create(user.FirstName, callBackUrl);

            _emailService.SendAsync(emailModel, _settings.EmailSettings, user.Email, _settings.EmailSettings.ResetPasswordEmailTemplate, _settings.EmailSettings.ResetPasswordSubject);

            return Ok(new { token });
        }

        [HttpPost]
        [Route("SetNewPassword")]
        public async Task<IActionResult> SetNewPassword(SetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound(new ErrorResponse(ErrorMessages.MissingUserEmail));
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return Unauthorized(new ErrorResponse(ErrorMessages.EmailNotConfirmed));
            }

            await _userManager.ResetPasswordAsync(user, model.PasswordToken, model.Password);

            return Ok();
        }
    }
}
