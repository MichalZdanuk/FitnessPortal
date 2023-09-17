namespace FitnessPortalAPI.Models.Calculators
{
    public class BMIDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float BMIScore { get; set; }
        public string BMICategory { get; set; }
    }
}
