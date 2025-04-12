namespace FitnessPortalAPI.Models.Trainings
{
    public record TrainingChartDataDto
    {
        public string? Date { get; set; }
        public float Payload { get; set; }
    }
}
