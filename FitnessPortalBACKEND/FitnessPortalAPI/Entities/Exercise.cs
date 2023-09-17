namespace FitnessPortalAPI.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfReps { get; set; }
        public float Payload { get; set; }
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}
