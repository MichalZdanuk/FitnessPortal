﻿using AutoMapper;
using FitnessPortalAPI.Authentication;
using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Mappings;
using FitnessPortalAPI.Models.Friendship;
using FitnessPortalAPI.Repositories;
using FitnessPortalAPI.Services;
using NSubstitute;
using Shouldly;

namespace FitnessPortalAPI.Tests.Services
{
    [TestClass]
    public class FriendshipServiceTests
    {
        private IAuthenticationContext _authenticationContext;
        private IFriendshipRepository _friendshipRepository;
        private IMapper _mapper;
        private FriendshipService _friendshipService;
        private int userId = 1;

		public FriendshipServiceTests()
        {
            _authenticationContext = Substitute.For<IAuthenticationContext>();
            _authenticationContext.UserId.Returns(userId);
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FitnessPortalMappingProfile>()));
            _friendshipService = new FriendshipService(_authenticationContext, _friendshipRepository, _mapper);
        }

        [TestMethod]
        public async Task SendFriendshipRequest_ValidRequest_ShouldCreateRequest()
        {
            // arrange
            int userToBeRequestedId = 2;
            var receiverUser = new User { Id = userToBeRequestedId };
            _friendshipRepository.GetUserByIdAsync(userToBeRequestedId).Returns(receiverUser);
            _friendshipRepository.FriendshipRequestExistsAsync(userId, userToBeRequestedId).Returns(false);
            _friendshipRepository.AreUsersFriendsAsync(userId, userToBeRequestedId).Returns(false);

            // act
            int requestId = await _friendshipService.SendFriendshipRequestAsync(userToBeRequestedId);

            // assert
            await _friendshipRepository.Received(1).CreateFriendshipRequestAsync(Arg.Is<FriendshipRequest>(f =>
                f.SenderId == userId &&
                f.ReceiverId == userToBeRequestedId));
        }

        [TestMethod]
        public async Task SendFriendshipRequest_ReceiverUserNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int userToBeRequestedId = 2;
            _friendshipRepository.GetUserByIdAsync(userToBeRequestedId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _friendshipService.SendFriendshipRequestAsync(userToBeRequestedId);


            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Receiver user does not exist.");
        }

        [TestMethod]
        public async Task SendFriendshipRequest_FriendshipRequestExists_ShouldThrowBadRequestException()
        {
            // arrange
            int userToBeRequestedId = 2;
            _friendshipRepository.GetUserByIdAsync(userToBeRequestedId).Returns(new User());
            _friendshipRepository.FriendshipRequestExistsAsync(userId, userToBeRequestedId).Returns(true);

            // act
            async Task Act() => await _friendshipService.SendFriendshipRequestAsync(userToBeRequestedId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Frienship request has already been sent between these users.");
        }

        [TestMethod]
        public async Task SendFriendshipRequest_AlreadyFriends_ShouldThrowBadRequestException()
        {
            // arrange
            int userToBeRequestedId = 2;
            _friendshipRepository.GetUserByIdAsync(userToBeRequestedId).Returns(new User());
            _friendshipRepository.AreUsersFriendsAsync(userId, userToBeRequestedId).Returns(true);

            // act
            async Task Act() => await _friendshipService.SendFriendshipRequestAsync(userToBeRequestedId);


            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("These users are already friends.");
        }

        [TestMethod]
        public async Task GetFriendshipRequests_NoFriends_ShouldReturnEmptyList()
        {
            // arrange
            List<FriendshipRequest> emptyList = new List<FriendshipRequest>();
            _friendshipRepository.GetFriendshipRequestsForUserAsync(userId).Returns(emptyList);

            // act
            var result = await _friendshipService.GetFriendshipRequestsAsync();

            // assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public async Task GetFriendshipRequests_SomeFriendshipRequests_ShouldReturnMappedList()
        {
            // arrange
            List<FriendshipRequest> requests = new List<FriendshipRequest>
            {
                new FriendshipRequest { Id = 1, SenderId = 2 },
                new FriendshipRequest { Id = 2, SenderId = 3 }
            };
            _friendshipRepository.GetFriendshipRequestsForUserAsync(userId).Returns(requests);

            // act
            var result = await _friendshipService.GetFriendshipRequestsAsync();

            // assert
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(requests.Count);
            foreach (var request in requests)
            {
                result.ShouldContain(r => r.Id == request.Id && r.SenderId == request.SenderId);
            }
        }

        [TestMethod]
        public async Task RejectFriendshipRequest_ValidRequest_ShouldRemoveFriendshipRequest()
        {
            // arrange
            int requestId = 2;
            var friendRequest = new FriendshipRequest { Id = requestId, ReceiverId = userId };
            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(friendRequest);

            // act
            await _friendshipService.RejectFriendshipRequestAsync(requestId);

            // assert
            await _friendshipRepository.Received(1).RemoveFriendshipRequestAsync(friendRequest);
        }

        [TestMethod]
        public async Task RejectFriendshipRequest_FriendshipRequestNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int invalidRequestId = 999;
            _friendshipRepository.GetFriendshipRequestAsync(invalidRequestId).Returns(Task.FromResult<FriendshipRequest?>(null));

            // act
            async Task Act() => await _friendshipService.RejectFriendshipRequestAsync(invalidRequestId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Friendship request not found.");
        }

        [TestMethod]
        public async Task RejectFriendshipRequest_UserNotAllowedToRemoveRequest_ShouldThrowForbiddenException()
        {
            // arrange
            int receiverId = 999;
            int requestId = 2;
            var friendRequest = new FriendshipRequest { Id = requestId, ReceiverId = receiverId };
            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(friendRequest);

            // act
            async Task Act() => await _friendshipService.RejectFriendshipRequestAsync(requestId);

            // assert
            var exception = await Should.ThrowAsync<ForbiddenException>(Act);
            exception.Message.ShouldBe("You are not allowed to remove someone else request!!!");
        }

        [TestMethod]
        public async Task AcceptFriendshipRequest_WhenRequestExistsAndCurrentUserIsReceiver_AddsFriendAndRemovesRequest()
        {
            // arrange
            int requestId = 2;

            var sender = new User { Id = 2, Username = "Sender" };
            var receiver = new User { Id = userId, Username = "Receiver" };
            var friendshipRequest = new FriendshipRequest { Id = requestId, Sender = sender, Receiver = receiver, ReceiverId = userId };

            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(friendshipRequest);

            // act
            await _friendshipService.AcceptFriendshipRequestAsync(requestId);

            // assert
            await _friendshipRepository.Received().AddFriendAsync(sender, receiver);
            await _friendshipRepository.Received().RemoveFriendshipRequestAsync(friendshipRequest);
        }

        [TestMethod]
        public async Task AcceptFriendshipRequest_WhenRequestNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int requestId = 2;
            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(Task.FromResult<FriendshipRequest?>(null));

            // act
            async Task Act() => await _friendshipService.AcceptFriendshipRequestAsync(requestId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Friendship request not found.");
        }

        [TestMethod]
        public async Task AcceptFriendshipRequest_WhenSenderOrReceiverIsNull_ShouldThrowBadRequestException()
        {
            // arrange
            int requestId = 2;
            var friendshipRequest = new FriendshipRequest { Id = requestId, ReceiverId = userId };

            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(friendshipRequest);

            // act
            async Task Act() => await _friendshipService.AcceptFriendshipRequestAsync(requestId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Invalid sender or receiver.");
        }

        [TestMethod]
        public async Task AcceptFriendshipRequest_WhenCurrentUserIsNotReceiver_ShouldThrowForbiddenException()
        {
            // arrange
            int requestId = 2;
            var sender = new User { Id = 2, Username = "Sender" };
            var receiver = new User { Id = 3, Username = "Receiver" };
            var friendshipRequest = new FriendshipRequest { Id = requestId, Sender = sender, Receiver = receiver, ReceiverId = 3 };

            _friendshipRepository.GetFriendshipRequestAsync(requestId).Returns(friendshipRequest);

            // act
            async Task Act() => await _friendshipService.AcceptFriendshipRequestAsync(requestId);

            // assert
            var exception = await Should.ThrowAsync<ForbiddenException>(Act);
            exception.Message.ShouldBe("You are not allowed to accept someone else request!!!");
        }

        [TestMethod]
        public async Task GetFriends_EmptyFriendList_ShouldReturnEmptyList()
        {
            // arrange
            var user = new User { Id = userId, Friends = new List<User>() };
            _friendshipRepository.GetUserByIdAsync(userId).Returns(user);

            // act
            var result = await _friendshipService.GetFriendsAsync();

            // assert
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public async Task GetFriends_ListOfFriends_ShouldReturnFriendList()
        {
            // arrange
            var friend1 = new User { Id = 2, Username = "Friend 1" };
            var friend2 = new User { Id = 3, Username = "Friend 2" };
            var user = new User { Id = userId, Friends = new List<User> { friend1, friend2 } };
            _friendshipRepository.GetUserByIdAsync(userId).Returns(user);

            // act
            var result = await _friendshipService.GetFriendsAsync();

            // assert
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(2);
            result.Any(f => f.Id == friend1.Id && f.Username == friend1.Username).ShouldBeTrue();
            result.Any(f => f.Id == friend2.Id && f.Username == friend2.Username).ShouldBeTrue();
        }

        [TestMethod]
        public async Task GetFriends_UserNotFound_ShouldThrowNotFoundException()
        {
            // arrange
            _friendshipRepository.GetUserByIdAsync(userId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _friendshipService.GetFriendsAsync();

            // assert
            var exception = await Should.ThrowAsync<NotFoundException>(Act);
            exception.Message.ShouldBe("User not found.");
        }

        [TestMethod]
        public async Task RemoveFriendship_WhenUsersAreFriends_CallsRepositoryRemoveFriendAsync()
        {
            // arrange
            int friendId = 2;
            var user = new User { Id = userId, Username = "User1" };
            var friend = new User { Id = friendId, Username = "Friend1" };

            _friendshipRepository.GetUserByIdAsync(userId).Returns(user);
            _friendshipRepository.GetUserByIdAsync(friendId).Returns(friend);
            _friendshipRepository.AreUsersFriendsAsync(userId, friendId).Returns(true);

            // act
            await _friendshipService.RemoveFriendshipAsync(friendId);

            // assert
            await _friendshipRepository.Received().RemoveFriendAsync(user, friend);
        }

        [TestMethod]
        public async Task RemoveFriendship_WhenUserIsNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int friendId = 2;

            // act
            _friendshipRepository.GetUserByIdAsync(userId).Returns(Task.FromResult<User?>(null));
            async Task Act() => await _friendshipService.RemoveFriendshipAsync(friendId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("User not found.");
        }

        [TestMethod]
        public async Task RemoveFriendship_WhenFriendIsNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int friendId = 2;

            var user = new User { Id = userId, Username = "User1" };

            _friendshipRepository.GetUserByIdAsync(userId).Returns(user);
            _friendshipRepository.GetUserByIdAsync(friendId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _friendshipService.RemoveFriendshipAsync(friendId);

            // assert

            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Friend not found.");
        }

        [TestMethod]
        public async Task RemoveFriendship_WhenUsersAreNotFriends_ShouldThrowBadRequestException()
        {
            // arrange
            int friendId = 2;

            var user = new User { Id = userId, Username = "User1" };
            var friend = new User { Id = friendId, Username = "Friend1" };

            _friendshipRepository.GetUserByIdAsync(userId).Returns(user);
            _friendshipRepository.GetUserByIdAsync(friendId).Returns(friend);
            _friendshipRepository.AreUsersFriendsAsync(userId, friendId).Returns(false);

            // act
            async Task Act() => await _friendshipService.RemoveFriendshipAsync(friendId);

            // assert

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("You are not friend with this user.");
        }

        [TestMethod]
        public async Task FindUsersWithPattern_OneMatchExists_ShouldReturnOnlyOneMatchingUser()
        {
            // arrange
            string pattern = "John";
            var matchingUsers = new List<User>
            {
                new User { Id = 2, Email = "John@Doe.mail.com" }
            };
            _friendshipRepository.FindUsersWithPatternAsync(userId, pattern).Returns(matchingUsers);

            // act
            var result = await _friendshipService.FindUsersWithPatternAsync(pattern);

            // assert
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(1);
            result.Any(u => u.Id == 2 && u.Email == "John@Doe.mail.com").ShouldBeTrue();
        }

        [TestMethod]
        public async Task FindUsersWithPattern_MultipleMatchesExist_ShouldReturnAllMatchingUsers()
        {
            // arrange
            string pattern = "John";
            var matchingUsers = new List<User>
            {
                new User { Id = 2, Email = "John@Doe.mail.com" },
                new User { Id = 3, Email = "John@Smith.mail.com" },
            };
            _friendshipRepository.FindUsersWithPatternAsync(userId, pattern).Returns(matchingUsers);

            // act
            var result = await _friendshipService.FindUsersWithPatternAsync(pattern);

            // assert
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(2);
            result.Any(u => u.Id == 2 && u.Email == "John@Doe.mail.com").ShouldBeTrue();
            result.Any(u => u.Id == 3 && u.Email == "John@Smith.mail.com").ShouldBeTrue();
        }

        [TestMethod]
        public async Task FindUsersWithPattern_NoMatchesExists_ShouldReturnEmptyList()
        {
            // arrange
            string pattern = "Unknown";
            _friendshipRepository.FindUsersWithPatternAsync(userId, pattern).Returns(new List<User>());

            // act
            var result = await _friendshipService.FindUsersWithPatternAsync(pattern);

            // assert
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public async Task GetFriendStatistics_FriendIsFoundAndUsersAreFriends_ShouldReturnFriendStatistics()
        {
            // arrange
            int friendId = 2;
            var friend = new User { 
                Id = friendId, 
                Username = "FriendName",
                Email = "example@email.com",
                Friends = new List<User>() };
            var friendTrainings = new List<Training>() 
            {
                new Training
                {
                    Id = 1,
                    DateOfTraining = new DateTime(2023, 5, 11),
                    NumberOfSeries = 4,
                    TotalPayload = 15000f,
                    UserId = userId,
                    Exercises = new List<Exercise>(),
                },
                new Training
                {
                    Id = 2,
                    DateOfTraining = new DateTime(2023, 5, 15),
                    NumberOfSeries = 4,
                    TotalPayload = 17000f,
                    UserId = userId,
                    Exercises = new List<Exercise>(),
                }
            };

            _friendshipRepository.GetUserByIdAsync(friendId).Returns(friend);
            _friendshipRepository.AreUsersFriendsAsync(userId, friendId).Returns(true);
            _friendshipRepository.GetFriendTrainingsAsync(friendId).Returns(friendTrainings);

            // act
            var result = await _friendshipService.GetFriendStatisticsAsync(friendId);

            // assert
            result.Username.ShouldBe(friend.Username);
            result.Email.ShouldBe(friend.Email);
            result.NumberOfFriends.ShouldBe(0);
            result.NumberOfTrainings.ShouldBe(2);
            result.LastThreeTrainings.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task GetFriendStatistics_FriendHasNoTrainings_ShouldReturnFriendStatisticsWithEmptyTrainingsList()
        {
            // arrange
            int friendId = 2;
            var friend = new User
            {
                Id = friendId,
                Username = "FriendName",
                Email = "example@email.com",
                Friends = new List<User>()
            };
            var friendTrainings = new List<Training>();

            _friendshipRepository.GetUserByIdAsync(friendId).Returns(friend);
            _friendshipRepository.AreUsersFriendsAsync(userId, friendId).Returns(true);
            _friendshipRepository.GetFriendTrainingsAsync(friendId).Returns(friendTrainings);

            // act
            var result = await _friendshipService.GetFriendStatisticsAsync(friendId);

            // assert
            result.Username.ShouldBe(friend.Username);
            result.Email.ShouldBe(friend.Email);
            result.NumberOfFriends.ShouldBe(0);
            result.NumberOfTrainings.ShouldBe(0);
            result.LastThreeTrainings.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task GetFriendStatistics_FriendIsNotFound_ShouldThrowBadRequestException()
        {
            // arrange
            int friendId = 2;
            _friendshipRepository.GetUserByIdAsync(friendId).Returns(Task.FromResult<User?>(null));

            // act
            async Task Act() => await _friendshipService.GetFriendStatisticsAsync(friendId);

            // assert
            var exception = await Should.ThrowAsync<BadRequestException>(Act);
            exception.Message.ShouldBe("Friend not found.");
        }

        [TestMethod]
        public async Task GetFriendStatistics_UsersAreNotFriends_ShouldThrowForbiddenException()
        {
            // arrange
            int friendId = 2;
            var friend = new User { Id = friendId };

            _friendshipRepository.GetUserByIdAsync(friendId).Returns(friend);
            _friendshipRepository.AreUsersFriendsAsync(userId, friendId).Returns(false);

            // act
            async Task Act() => await _friendshipService.GetFriendStatisticsAsync(friendId);

            // assert
            var exception = await Should.ThrowAsync<ForbiddenException>(Act);
            exception.Message.ShouldBe("Given user is not your friend.");
        }
    }
}
