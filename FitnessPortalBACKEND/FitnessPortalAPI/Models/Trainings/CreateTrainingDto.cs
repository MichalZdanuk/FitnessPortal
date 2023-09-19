namespace FitnessPortalAPI.Models.Trainings
{
    public class CreateTrainingDto
    {
        public int NumberOfSeries { get; set; }
        public List<CreateExerciseDto> Exercises { get; set; }
    }
}
