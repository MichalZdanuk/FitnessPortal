using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Models.Friendship
{
    public record FriendProfileDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public float Weight { get; set; }
        public float Height { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int NumberOfFriends { get; set; }
        public int NumberOfTrainings { get; set; }
        public List<TrainingDto> LastThreeTrainings { get; set; }

    }
}
