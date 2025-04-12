namespace FitnessPortalAPI.Models.Trainings
{
    public record TrainingStatsDto
    {
        public int NumberOfTrainings { get; set; }
        public TrainingDto? BestTraining { get; set; }
        public TrainingDto? MostRecentTraining { get; set; }
    }
}
