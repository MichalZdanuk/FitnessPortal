namespace FitnessPortalAPI
{
    public class Calculator
    {
        public void CalculateBmi(float height, float weight, out float bmiIndex, out string bmiCategory)
        {
            bmiIndex = weight / (height*height/10000);
            if (bmiIndex < 18.5)
            {
                bmiCategory = "underweight";
            } else if (18.5 <= bmiIndex && bmiIndex < 25)
            {
                bmiCategory = "normalweight";
            } else if (25 <= bmiIndex && bmiIndex < 30)
            {
                bmiCategory = "overweight";

            } else
            {
                bmiCategory = "obesity";
            }
        }
    }
}
