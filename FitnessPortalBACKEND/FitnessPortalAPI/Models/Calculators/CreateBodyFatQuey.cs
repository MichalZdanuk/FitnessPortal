namespace FitnessPortalAPI.Models.Calculators
{
	public class CreateBodyFatQuery
    {
        public Sex? Sex { get; set; }
        public float Height { get; set; }
        public float Waist { get; set; }
        public float Hip { get; set; }
        public float Neck { get; set; }
    }
}
