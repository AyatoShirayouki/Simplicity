using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Simplicity.DataContracts;
using Simplicity.Entities;
using Simplicity.Helpers;
using Simplicity.Services.ServicesInterfaces;
using Simplicity.ViewModels;

namespace Simplicity.Controllers
{
    [Route("api/account")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IOptions<AppSettings> _appSettings;

        public AccountController(
            IAccountService accountService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUsersService usersService)
        {
            _accountService = accountService;
            _appSettings = appSettings;
            _mapper = mapper;
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromForm]string username, [FromForm]string password)
        {
            var user = _accountService.Authenticate(username, password);

            if (user == null)
                return NotFound(new { message = "Username or password is incorrect" });
            var tokenString = _accountService.CreateToken(user, _appSettings).Result;

            if (string.IsNullOrEmpty(tokenString))
            {
                return BadRequest();
            }
           
            // return basic user info (without password) and token to store client side
            return Ok(new { id = user.ID, userName = user.Username, role = user.Role, token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromForm]UsersEditVM userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);
            user.Role = Role.User;

            _usersService.Save(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromForm]ChangePasswordVM changePasswordDto)
        {
            var userIDAsString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userID = 0;
            int.TryParse(userIDAsString, out userID);

            if (userID == 0)
            {
                return NotFound(new { message = "Invalid User ID" });
            }

            var user = _usersService.GetById(userID);
            // map dto to entity
            if (user.Password != changePasswordDto.OldPassword)
            {
                return BadRequest(new { message = "Invalid Old password. Please fill again" });
            }

            user.Password = changePasswordDto.NewPassword;
           
            _usersService.Save(user);
            return Ok();
        }
    }
}