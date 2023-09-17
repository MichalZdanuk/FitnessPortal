namespace FitnessPortalAPI.Models.Trainings
{
    public class ExerciseDto
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfReps { get; set; }
        public float Payload { get; set; }
    }
}
