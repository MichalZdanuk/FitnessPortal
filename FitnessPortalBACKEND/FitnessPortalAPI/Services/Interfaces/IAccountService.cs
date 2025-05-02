using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Services.Interfaces;
public interface IAccountService
{
	Task<string> GenerateJwtAsync(LoginUserDto dto);
	Task RegisterUserAsync(RegisterUserDto dto);
	Task<UserProfileInfoDto> GetProfileInfoAsync();
	Task<string> UpdateProfileAsync(UpdateUserDto dto);
}
