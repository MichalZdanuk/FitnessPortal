namespace FitnessPortalAPI.Models.Friendship
{
    public class CreateFriendshipRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
