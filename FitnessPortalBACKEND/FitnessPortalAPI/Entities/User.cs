namespace FitnessPortalAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }

        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<BMI> BMIs { get; set; }
        public virtual List<Training> Trainings { get; set; }
        public virtual List<User> Friends { get; set; }
        public virtual List<FriendshipRequest> SentFriendRequests { get; set; }
        public virtual List<FriendshipRequest> ReceivedFriendRequests { get; set; }
    }
}
