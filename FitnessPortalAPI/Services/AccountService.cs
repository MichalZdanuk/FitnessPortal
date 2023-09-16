using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.UserActions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static FitnessPortalAPI.Services.TokenStore;

namespace FitnessPortalAPI.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginUserDto dto);
        void RegisterUser(RegisterUserDto dto);
        UserProfileInfoDto GetProfileInfo(int userId);
        Task<string> UpdateProfile(UpdateUserDto dto, int userId, string previousToken);
    }
    public class AccountService : IAccountService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ITokenStore _tokenStore;
        public AccountService(FitnessPortalDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ITokenStore tokenStore)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _tokenStore = tokenStore;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Username = dto.Username,
                DateOfBirth = dto.DateOfBirth,
                Weight = dto.Weight,
                Height = dto.Height,
                RoleId  = dto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                throw new BadRequestException("Invalid username or password");
            

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");
            

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public UserProfileInfoDto GetProfileInfo(int userId)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app
            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var userInfo = new UserProfileInfoDto()
            {
                Username = user.Username,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Weight = user.Weight,
                Height = user.Height,
                NumberOfFriends = user.Friends.Count(),
            };

            return userInfo;
        }

        public async Task<string> UpdateProfile(UpdateUserDto dto, int userId, string previousToken)
        {
            var userToBeUpdated = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userToBeUpdated == null)
                throw new ForbiddenException("You are not allowed to update profile");

            await _tokenStore.BlacklistTokenAsync(previousToken); // add token to blacklist

            userToBeUpdated.Email= dto.Email;
            userToBeUpdated.Username= dto.Username;
            userToBeUpdated.DateOfBirth = dto.DateOfBirth;
            userToBeUpdated.Weight = dto.Weight;
            userToBeUpdated.Height = dto.Height;

            _context.SaveChanges();

            string newToken = GenerateJwtWhenProfileUpdated(dto.Email);

            return newToken;
        }

        /*temporary duplicated code (very similar to GenearteJwt)*/
        public string GenerateJwtWhenProfileUpdated(string userEmail)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }


    }
}
