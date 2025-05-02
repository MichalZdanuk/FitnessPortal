using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/account")]
    [ApiController]
    public class AccountController(IAccountService accountService)
        : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await accountService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto dto)
        {
            string token = await accountService.GenerateJwtAsync(dto);

            if (token is null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(token);
        }

        [Authorize]
        [HttpGet("profile-info")]
        public async Task<ActionResult<UserProfileInfoDto>> GetMyProfileInfo()
        {
            var userInfo = await accountService.GetProfileInfoAsync();

            return Ok(userInfo);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<string>> UpdateMyProfile([FromBody] UpdateUserDto dto)
        {
            string newToken = await accountService.UpdateProfileAsync(dto);
            if (newToken is null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(newToken);
        }
    }
}
