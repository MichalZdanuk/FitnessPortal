using AutoMapper;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Repositories;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ITokenStore _tokenStore;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ITokenStore tokenStore, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _tokenStore = tokenStore;
            _mapper = mapper;
        }

        public async Task RegisterUserAsync(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.RoleId = (int)Roles.User;
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            await _accountRepository.CreateUserAsync(newUser);
        }

        public async Task<string> GenerateJwtAsync(LoginUserDto dto)
        {
            var user = await _accountRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            return GenerateJwtToken(user);
        }

        public async  Task<UserProfileInfoDto> GetProfileInfoAsync(int userId)
        {
            var user = await _accountRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var userProfileInfoDto = _mapper.Map<UserProfileInfoDto>(user);
            userProfileInfoDto.NumberOfFriends = user.Friends.Count();

            return userProfileInfoDto;
        }

        public async Task<string> UpdateProfileAsync(UpdateUserDto dto, int userId, string previousToken)
        {
            var userToBeUpdated = await _accountRepository.GetUserByIdAsync(userId);

            if (userToBeUpdated == null)
                throw new ForbiddenException("You are not allowed to update profile");

            await _tokenStore.BlacklistTokenAsync(previousToken);
            _mapper.Map(dto, userToBeUpdated);
            await _accountRepository.UpdateUserAsync(userToBeUpdated);

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
