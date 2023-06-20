namespace FitnessPortalAPI.Services
{
    public interface ITokenStore
    {
        Task BlacklistTokenAsync(string token);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }
        
    public class TokenStore : ITokenStore
    {
        private readonly HashSet<string> _blacklistedTokens;
        public TokenStore()
        {
            _blacklistedTokens = new HashSet<string>();
        }
        public Task BlacklistTokenAsync(string token)
        {
            _blacklistedTokens.Add(token);
            return Task.CompletedTask;
        }

        public Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return Task.FromResult(_blacklistedTokens.Contains(token));
        }
    }
}
