﻿namespace FitnessPortalAPI
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
        public float CalculateBMR(float weight, float height, float age, string sex)
        {
            float bmrResult = 0.0f;

            switch (sex)
            {
                case "male":
                    bmrResult = 88.362f + (13.397f * weight) + (4.799f * height) - (5.677f * age);
                    break;
                case "female":
                    bmrResult = 447.593f + (9.247f * weight) + (3.098f * height) - (4.330f * age);
                    break;
                default:
                    break;
            }

            return bmrResult;
        }

        public float CalculateBodyFat(float height, float waist, float neck, float hip, string sex)
        {
            float bodyFatResult = 0.0f;

            switch (sex)
            {
                case "male":
                    bodyFatResult = (float)(495.0f /(1.0324f-0.19077f*Math.Log10(waist-neck)+0.15456f*Math.Log10(height)) - 450.0f);
                    break;
                case "female":
                    bodyFatResult = (float)(495.0f / (1.29579f - 0.35004f * Math.Log10(waist + hip - neck) + 0.22100f * Math.Log10(height)) - 450.0f);
                    break;
                default:
                    break;
            }

            return bodyFatResult;
        }
    }
}
