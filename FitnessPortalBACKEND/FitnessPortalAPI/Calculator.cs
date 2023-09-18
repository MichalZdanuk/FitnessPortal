using FitnessPortalAPI.Constants;

namespace FitnessPortalAPI
{
    public class Calculator
    {
        public void CalculateBMI(float height, float weight, out float bmiIndex, out BMICategory bmiCategory)
        {
            if (height <= 0 || weight <= 0)
            {
                throw new ArgumentException("Height and weight must be positive values.");
            }

            float maxHeight = 250; // Maximum valid height in centimeters
            float minHeight = 100; // Minimum valid height in centimeters
            float maxWeight = 300; // Maximum valid weight in kilograms
            float minWeight = 20;  // Minimum valid weight in kilograms

            if (height > maxHeight || height < minHeight || weight > maxWeight || weight < minWeight)
            {
                throw new ArgumentException("Height and weight values are out of valid range.");
            }

            bmiIndex = weight / (height * height / 10000);
            if (bmiIndex < 18.5)
            {
                bmiCategory = BMICategory.Underweight;
            }
            else if (18.5 <= bmiIndex && bmiIndex < 25)
            {
                bmiCategory = BMICategory.Normalweight;
            }
            else if (25 <= bmiIndex && bmiIndex < 30)
            {
                bmiCategory = BMICategory.Overweight;

            }
            else
            {
                bmiCategory = BMICategory.Obesity;
            }
        }
        public float CalculateBMR(float height, float weight, int age, Sex sex)
        {
            if (weight <= 0 || height <= 0 || age <= 0)
            {
                throw new ArgumentException("Weight, height, and age must be positive values.");
            }

            if (!Enum.IsDefined(typeof(Sex), sex))
            {
                throw new ArgumentException("Invalid value for 'sex' parameter.");
            }

            float bmrResult = 0.0f;

            switch (sex)
            {
                case Sex.Male:
                    bmrResult = 88.362f + (13.397f * weight) + (4.799f * height) - (5.677f * age);
                    break;
                case Sex.Female:
                    bmrResult = 447.593f + (9.247f * weight) + (3.098f * height) - (4.330f * age);
                    break;
                default:
                    break;
            }

            return bmrResult;
        }

        public float CalculateBodyFat(float height, float waist, float neck, float hip, Sex sex)
        {
            if (height <= 0 || waist <= 0 || neck <= 0 || hip <= 0)
            {
                throw new ArgumentException("Height, waist, neck, and hip measurements must be positive values.");
            }

            if (!Enum.IsDefined(typeof(Sex), sex))
            {
                throw new ArgumentException("Invalid value for 'sex' parameter.");
            }

            float bodyFatResult = 0.0f;

            switch (sex)
            {
                case Sex.Male:
                    bodyFatResult = (float)(495.0f / (1.0324f - 0.19077f * Math.Log10(waist - neck) + 0.15456f * Math.Log10(height)) - 450.0f);
                    break;
                case Sex.Female:
                    bodyFatResult = (float)(495.0f / (1.29579f - 0.35004f * Math.Log10(waist + hip - neck) + 0.22100f * Math.Log10(height)) - 450.0f);
                    break;
                default:
                    break;
            }

            return bodyFatResult;
        }
    }
}
