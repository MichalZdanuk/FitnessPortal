using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Services.Interfaces;
public interface IAccountService
{
	Task<string> GenerateJwtAsync(LoginUserDto dto);
	Task RegisterUserAsync(RegisterUserDto dto);
	Task<UserProfileInfoDto> GetProfileInfoAsync(int userId);
	Task<string> UpdateProfileAsync(UpdateUserDto dto, int userId, string previousToken);
}
