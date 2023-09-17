namespace FitnessPortalAPI.Entities
{
    public class BMI
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float BMIScore { get; set; }
        public string BMICategory { get; set; } = string.Empty;
        public float Height { get; set; }
        public float Weight { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
