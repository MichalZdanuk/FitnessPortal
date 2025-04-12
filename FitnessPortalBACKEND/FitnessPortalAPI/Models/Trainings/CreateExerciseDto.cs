namespace FitnessPortalAPI.Models.Trainings
{
    public record CreateExerciseDto
    {
        public string? Name { get; set; }
        public int NumberOfReps { get; set; }
        public float Payload { get; set; }
    }
}
