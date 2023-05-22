namespace FitnessPortalAPI.Entities
{
    public class BMI
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int BMIScore { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
