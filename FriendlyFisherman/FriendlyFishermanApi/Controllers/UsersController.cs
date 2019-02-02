using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.Services.Abstraction;

namespace FriendlyFishermanApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(IUserService userService, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
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
    }
}