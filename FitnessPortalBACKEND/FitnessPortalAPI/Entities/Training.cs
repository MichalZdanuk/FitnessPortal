namespace FitnessPortalAPI.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime DateOfTraining { get; set; }
        public int NumberOfSeries { get; set; }
        public float TotalPayload { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Exercise> Exercises { get; set; }
    }
}
