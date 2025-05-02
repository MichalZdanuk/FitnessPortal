using FitnessPortalAPI.Authentication;
using FitnessPortalAPI.Models.UserProfileActions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FitnessPortalAPI.Services;

public class AccountService(IAuthenticationContext authenticationContext,
		IHttpContextAccessor httpContextAccessor,
		IAccountRepository accountRepository,
		IPasswordHasher<User> passwordHasher,
		AuthenticationSettings authenticationSettings,
		ITokenStore tokenStore,
		IMapper mapper)
		: IAccountService
{
	public async Task RegisterUserAsync(RegisterUserDto dto)
	{
		var newUser = mapper.Map<User>(dto);
		newUser.RoleId = (int)Roles.User;
		var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
		newUser.PasswordHash = hashedPassword;

		await accountRepository.CreateUserAsync(newUser);
	}

	public async Task<string> GenerateJwtAsync(LoginUserDto dto)
	{
		var user = await accountRepository.GetUserByEmailAsync(dto.Email);

		if (user == null)
			throw new BadRequestException("Invalid username or password");

		var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

		if (result == PasswordVerificationResult.Failed)
			throw new BadRequestException("Invalid username or password");

		return GenerateJwtToken(user);
	}

	public async Task<UserProfileInfoDto> GetProfileInfoAsync()
	{
		var user = await accountRepository.GetUserByIdAsync(authenticationContext.UserId);

		if (user == null)
			throw new NotFoundException("User not found");

		var userProfileInfoDto = mapper.Map<UserProfileInfoDto>(user);
		userProfileInfoDto.NumberOfFriends = user.Friends.Count();

		return userProfileInfoDto;
	}

	public async Task<string> UpdateProfileAsync(UpdateUserDto dto)
	{
		var previousToken = httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

		var userToBeUpdated = await accountRepository.GetUserByIdAsync(authenticationContext.UserId);

		if (userToBeUpdated == null)
			throw new ForbiddenException("You are not allowed to update profile");

		await tokenStore.BlacklistTokenAsync(previousToken);
		mapper.Map(dto, userToBeUpdated);
		await accountRepository.UpdateUserAsync(userToBeUpdated);

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

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

		var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
			authenticationSettings.JwtIssuer,
			claims,
			expires: expires,
			signingCredentials: cred);

		var tokenHandler = new JwtSecurityTokenHandler();

		return tokenHandler.WriteToken(token);
	}
}
