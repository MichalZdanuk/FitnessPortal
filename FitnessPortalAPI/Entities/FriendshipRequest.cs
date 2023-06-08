namespace FitnessPortalAPI.Entities
{
    public class FriendshipRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SendDate { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
