namespace FitnessPortalAPI.Models.Friendship
{
    public record MatchingUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
