namespace FitnessPortalAPI.Models.Friendship
{
    public record CreateFriendshipRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
