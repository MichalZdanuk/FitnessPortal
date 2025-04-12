namespace FitnessPortalAPI.Models.Trainings
{
    public record CreateTrainingDto
    {
        public int NumberOfSeries { get; set; }
        public List<CreateExerciseDto> Exercises { get; set; }
    }
}
