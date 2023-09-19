namespace FitnessPortalAPI.Models.Trainings
{
    public class CreateExerciseDto
    {
        public string? Name { get; set; }
        public int NumberOfReps { get; set; }
        public float Payload { get; set; }
    }
}
