using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels;
using Users.Services.Abstraction;

namespace FriendlyFishermanApi.Controllers
{
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

        [HttpGet]
        [Route("/Authenticate/{username}/{password}")]
        public IActionResult Authenticate(string username, string password)
        {
            var result = _signInManager.PasswordSignInAsync(username, password, false, false).Result;

            if (result == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return Ok(_userService.GetAuth(username));
            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(_userService.GetAuth(user.UserName));
            }
            else
            {
                return NotFound();
            }
        }
    }
}