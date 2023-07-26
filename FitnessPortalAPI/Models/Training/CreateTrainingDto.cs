namespace FitnessPortalAPI.Models.Training
{
    public class CreateTrainingDto
    {
        public int NumberOfSeries { get; set; }
        public float TotalPayload { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
    }
}
