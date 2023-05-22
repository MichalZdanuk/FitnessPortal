namespace FitnessPortalAPI.Entities
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public int NumberOfTrainings { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<BMI> BMIs { get; set; }
    }
}
