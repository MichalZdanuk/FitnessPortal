namespace FitnessPortalAPI.Models.Friendship
{
    public class FriendshipDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public DateTime SendDate { get; set; }
    }
}
