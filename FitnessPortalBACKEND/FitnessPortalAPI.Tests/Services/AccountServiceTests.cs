using AutoMapper;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;

namespace FitnessPortalAPI.Tests.Services
{
    [TestClass]
    public class AccountServiceTests
    {
        private IAccountRepository _accountRepository;
        private IPasswordHasher<User> _passwordHasher;
        private AuthenticationSettings _authenticationSettings;
        private ITokenStore _tokenStore;
        private IMapper _mapper;
        private AccountService _accountService;

        public AccountServiceTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher<User>>();
            _authenticationSettings = new AuthenticationSettings
            {
                JwtKey = "PRIVATE_KEY_DONT_SHARE",
                JwtExpireDays = 1,
                JwtIssuer = "http://fitnessportalapi.com",
            };
            _tokenStore = Substitute.For<ITokenStore>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FitnessPortalMappingProfile>()));
            _accountService = new AccountService(
                _accountRepository,
                _passwordHasher,
                _authenticationSettings,
                _tokenStore,
                _mapper
            );
        }

        [TestMethod]
        public async Task RegisterUserAsync_ValidDto_ShouldCreateUser()
        {
            // arrange
            var registerUserDto = new RegisterUserDto
            {
                Email = "test@example.com",
                Username = "testuser",
                Password = "password",
                ConfirmPassword = "password",
                DateOfBirth = new DateTime(1990, 1, 1),
                Weight = 70.0f,
                Height = 180.0f,
            };

            // act
            await _accountService.RegisterUserAsync(registerUserDto);

            // assert
            await _accountRepository.Received(1).CreateUserAsync(Arg.Is<User>(u =>
                u.Email == registerUserDto.Email &&
                u.Username == registerUserDto.Username &&
                u.DateOfBirth == registerUserDto.DateOfBirth &&
                u.Weight == registerUserDto.Weight &&
                u.Height == registerUserDto.Height));
        }

        [TestMethod]
        public async Task GenerateJwtAsync_ValidLogin_ShouldGenerateJwtToken()
        {
            // arrange
            var loginUserDto = new LoginUserDto
            {
                Email = "test@example.com",
                Password = "password",
            };

            var user = new User
            {
                Id = 1,
                Email = loginUserDto.Email,
                Username = "testuser",
                RoleId = (int)Roles.User,
                Role = new Role { Name = "User" },
                DateOfBirth = new DateTime(2001, 7, 10),
            };

            _accountRepository.GetUserByEmailAsync(loginUserDto.Email).Returns(user);
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password)
                .Returns(PasswordVerificationResult.Success);

            // act
            var jwtToken = await _accountService.GenerateJwtAsync(loginUserDto);

            // assert
            jwtToken.ShouldNotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task GenerateJwtAsync_InvalidLogin_ShouldThrowBadRequestException()
        {
            // arrange
            var loginUserDto = new LoginUserDto
            {
                Email = "test@example.com",
                Password = "password",
            };

            _accountRepository.GetUserByEmailAsync(loginUserDto.Email).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _accountService.GenerateJwtAsync(loginUserDto);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Invalid username or password");
        }

        [TestMethod]
        public async Task GenerateJwtAsync_InvalidPassword_ShouldThrowBadRequestException()
        {
            // arrange
            var loginUserDto = new LoginUserDto
            {
                Email = "test@example.com",
                Password = "password",
            };

            var user = new User
            {
                Id = 1,
                Email = loginUserDto.Email,
                Username = "testuser",
                RoleId = (int)Roles.User,
            };

            _accountRepository.GetUserByEmailAsync(loginUserDto.Email).Returns(user);
            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password)
                .Returns(PasswordVerificationResult.Failed);

            // act
            async Task Act() => await _accountService.GenerateJwtAsync(loginUserDto);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Invalid username or password");
        }

        [TestMethod]
        public async Task GetProfileInfoAsync_ValidUserId_ShouldReturnUserProfileInfoDto()
        {
            // arrange
            var userId = 1;
            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                Username = "testuser",
                DateOfBirth = new DateTime(2000, 1, 1),
                Weight = 70f,
                Height = 175.2f,
                Friends = new List<User> { },
            };

            _accountRepository.GetUserByIdAsync(userId).Returns(user);

            // act
            var result = await _accountService.GetProfileInfoAsync(userId);

            // assert
            result.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task GetProfileInfoAsync_InvalidUserId_ShouldThrowNotFoundException()
        {
            // arrange
            var invalidUserId = 999;
            _accountRepository.GetUserByIdAsync(invalidUserId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _accountService.GetProfileInfoAsync(invalidUserId);

            // assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("User not found");
        }

        [TestMethod]
        public async Task UpdateProfileAsync_ValidDto_ShouldUpdateUserAndReturnJwtToken()
        {
            // arrange
            var userId = 1;
            var updateUserDto = new UpdateUserDto
            {
                Username = "newusername",
                Email = "newemail@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Weight = 75.0f,
                Height = 185.0f
            };

            var userToBeUpdated = new User
            {
                Id = userId,
                Username = "username",
                Email = "email",
                RoleId = (int)Roles.User,
                Role = new Role { Name = "User" },
                DateOfBirth = new DateTime(2001, 7, 10),
                Weight = 70.0f,
                Height = 180.0f,
            };

            var previousToken = "previousToken";

            _accountRepository.GetUserByIdAsync(userId).Returns(userToBeUpdated);

            // act
            var result = await _accountService.UpdateProfileAsync(updateUserDto, userId, previousToken);

            // assert
            result.ShouldNotBeNull();
            userToBeUpdated.Username.ShouldBe(updateUserDto.Username);
            userToBeUpdated.Email.ShouldBe(updateUserDto.Email);
            userToBeUpdated.DateOfBirth.ShouldBe(updateUserDto.DateOfBirth);
            userToBeUpdated.Weight.ShouldBe(updateUserDto.Weight);
            userToBeUpdated.Height.ShouldBe(updateUserDto.Height);
        }

        [TestMethod]
        public async Task UpdateProfileAsync_UserNotFound_ShouldThrowForbiddenException()
        {
            // arrange
            var userId = 1;
            var updateUserDto = new UpdateUserDto
            {
                Username = "newusername",
                Email = "newemail@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Weight = 75.0f,
                Height = 185.0f
            };

            var previousToken = "previousToken";

            _accountRepository.GetUserByIdAsync(userId).Returns(Task.FromResult<User?>(null));

            // act
            Func<Task> act = async () => await _accountService.UpdateProfileAsync(updateUserDto, userId, previousToken);

            // assert
            await Should.ThrowAsync<ForbiddenException>(act);
        }
    }
}
