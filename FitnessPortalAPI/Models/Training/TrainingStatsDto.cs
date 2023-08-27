namespace FitnessPortalAPI.Models.Training
{
    public class TrainingStatsDto
    {
        public int NumberOfTrainings { get; set; }
        public TrainingDto BestTraining { get; set; }
        public TrainingDto MostRecentTraining { get; set; }
    }
}
