namespace FitnessPortalAPI.Models.UserProfileActions
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
