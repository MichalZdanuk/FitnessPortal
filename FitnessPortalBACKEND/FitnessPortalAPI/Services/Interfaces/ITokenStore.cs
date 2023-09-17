namespace FitnessPortalAPI.Services.Interfaces
{
    public interface ITokenStore
    {
        Task BlacklistTokenAsync(string token);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }
}
