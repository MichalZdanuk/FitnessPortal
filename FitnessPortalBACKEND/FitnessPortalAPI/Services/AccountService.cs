using AutoMapper;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessPortalAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ITokenStore _tokenStore;
        private readonly IMapper _mapper;

        public AccountService(FitnessPortalDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ITokenStore tokenStore, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _tokenStore = tokenStore;
            _mapper = mapper;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.RoleId = (int)Roles.User;
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

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            return GenerateJwtToken(user);
        }

        public UserProfileInfoDto GetProfileInfo(int userId)
        {
            var user = _context.Users
                .Include(u => u.Friends)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            return _mapper.Map<UserProfileInfoDto>(user);
        }

        public async Task<string> UpdateProfile(UpdateUserDto dto, int userId, string previousToken)
        {
            var userToBeUpdated = _context.Users
                                        .Include(u => u.Role)                    
                                        .FirstOrDefault(u => u.Id == userId);
            if (userToBeUpdated == null)
                throw new ForbiddenException("You are not allowed to update profile");

            await _tokenStore.BlacklistTokenAsync(previousToken);
            _mapper.Map(dto, userToBeUpdated);
            _context.SaveChanges();

            return GenerateJwtToken(userToBeUpdated);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd") ?? "N/A")
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
