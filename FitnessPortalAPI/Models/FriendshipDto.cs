namespace FitnessPortalAPI.Models
{
    public class FriendshipDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderName { get; set; }
        public DateTime SendDate { get; set; }
    }
}
