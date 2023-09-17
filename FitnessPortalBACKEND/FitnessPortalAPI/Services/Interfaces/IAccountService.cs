using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Services.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginUserDto dto);
        void RegisterUser(RegisterUserDto dto);
        UserProfileInfoDto GetProfileInfo(int userId);
        Task<string> UpdateProfile(UpdateUserDto dto, int userId, string previousToken);
    }
}
