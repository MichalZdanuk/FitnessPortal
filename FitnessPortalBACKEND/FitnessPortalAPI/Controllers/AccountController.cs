using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Utilities;

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
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _accountService.RegisterUserAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto dto)
        {
            string token = await _accountService.GenerateJwtAsync(dto);

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
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var userInfo = await _accountService.GetProfileInfoAsync(userId);

            return Ok(userInfo);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<string>> UpdateMyProfile([FromBody] UpdateUserDto dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);
            var previousToken = _contextAccessor.HttpContext!.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            string newToken = await _accountService.UpdateProfileAsync(dto, userId, previousToken);
            if (newToken == null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(newToken);
        }
    }
}
