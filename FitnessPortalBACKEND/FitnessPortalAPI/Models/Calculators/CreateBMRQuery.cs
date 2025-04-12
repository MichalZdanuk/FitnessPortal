namespace FitnessPortalAPI.Models.Calculators
{
	public class CreateBMRQuery
    {
        public Sex? Sex { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
    }
}
