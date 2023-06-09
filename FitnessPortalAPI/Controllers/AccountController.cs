﻿using FitnessPortalAPI.Models.UserActions;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AccountController(IAccountService accountService, IHttpContextAccessor contextAccessor)
        {
            _accountService = accountService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto dto)
        {
            string token = _accountService.GenerateJwt(dto);

            if(token == null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(token);
        }
        [Authorize]
        [HttpGet("profile-info")]
        public ActionResult<UserProfileInfoDto> GetMyProfileInfo()
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userInfo = _accountService.GetProfileInfo(userId);

            return Ok(userInfo);
        }

        [Authorize]
        [HttpPost("update-profile")]
        public async Task<ActionResult<string>> UpdateMyProfile([FromBody] UpdateUserDto dto)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var previousToken = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            string newToken = await _accountService.UpdateProfile(dto, userId, previousToken);
            if(newToken == null) 
            {
                return BadRequest("Invalid user data");
            }

            return Ok(newToken);
        }
    }
}
