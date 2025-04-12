namespace FitnessPortalAPI.Models.Calculators
{
	public record BMIDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float BMIScore { get; set; }
        public BMICategory BMICategory { get; set; }
    }
}
