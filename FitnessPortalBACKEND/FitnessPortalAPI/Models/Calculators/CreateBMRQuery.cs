namespace FitnessPortalAPI.Models.Calculators
{
    public class CreateBMRQuery
    {
        public string Sex { get; set; } = string.Empty;
        public float Height { get; set; }
        public float Weight { get; set; }
        public float Age { get; set; }
    }
}
