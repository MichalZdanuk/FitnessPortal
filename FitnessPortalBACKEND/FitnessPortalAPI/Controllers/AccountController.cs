using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/account")]
    [ApiController]
    public class AccountController(IAccountService accountService, IHttpContextAccessor contextAccessor) : ControllerBase
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

            if (token == null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(token);
        }

        [Authorize]
        [HttpGet("profile-info")]
        public async Task<ActionResult<UserProfileInfoDto>> GetMyProfileInfo()
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var userInfo = await accountService.GetProfileInfoAsync(userId);

            return Ok(userInfo);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<string>> UpdateMyProfile([FromBody] UpdateUserDto dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);
            var previousToken = contextAccessor.HttpContext!.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            string newToken = await accountService.UpdateProfileAsync(dto, userId, previousToken);
            if (newToken == null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(newToken);
        }
    }
}
