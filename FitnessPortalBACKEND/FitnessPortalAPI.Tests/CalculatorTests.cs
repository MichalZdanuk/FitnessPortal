using FitnessPortalAPI.Constants;
using Shouldly;

namespace FitnessPortalAPI.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        [DataRow(250, 45, BMICategory.Underweight)] // Minimum valid height and weight
        [DataRow(200, 300, BMICategory.Obesity)]    // Maximum valid height and weight
        [DataRow(170, 55, BMICategory.Normalweight)] // Typical normal weight
        [DataRow(170, 85, BMICategory.Overweight)]   // Typical overweight
        [DataRow(170, 115, BMICategory.Obesity)]      // Typical obesity
        public void CalculateBMI_ValidData_ReturnCorrectResult(float height, float weight, BMICategory expectedCategory)
        {
            // arrange
            float bmiIndex;
            BMICategory bmiCategory;

			// act
			Calculator.CalculateBMI(height, weight, out bmiIndex, out bmiCategory);

            // assert
            bmiCategory.ShouldBe(expectedCategory);
        }

        [TestMethod]
        [DataRow(-160.0f,55.0f)]
        [DataRow(175.0f, -55.0f)]
        [DataRow(0.0f, -5.0f)]
        public void CalculateBMI_InvalidData_ThrowArgumentException(float height, float weight)
        {
            // arrange

            // act
            Action act = () => Calculator.CalculateBMI(height, weight, out _, out _);

            // assert
            var exception = Should.Throw<ArgumentException>(act);
            exception.Message.ShouldBe("Height and weight must be positive values.");
        }

        [TestMethod]
        [DataRow(255.0f, 55.0f)]
        [DataRow(99.0f, 45.0f)]
        [DataRow(100.0f, 301.0f)]
        [DataRow(100.0f, 19.0f)]
        [DataRow(250.0f, 19.0f)]
        public void CalculateBMI_DataOutOfRange_ThrowArgumentException(float height, float weight)
        {
            // arrange

            // act
            Action act = () => Calculator.CalculateBMI(height, weight, out _, out _);

            // assert
            var exception = Should.Throw<ArgumentException>(act);
            exception.Message.ShouldBe("Height and weight values are out of valid range.");
        }

        [TestMethod]
        [DataRow(175f, 72.1f, 22, Sex.Male, 1769.2166f)]
        [DataRow(167.5f, 57.5f, 21, Sex.Female, 1407.2804f)]
        public void CalculateBMR_ValidData_ReturnCorrectResult(float height, float weight, int age, Sex sex, float expectedBMRResult)
        {
            // arrange
            float bmrResult = 0f;

            // act
            bmrResult = Calculator.CalculateBMR(height, weight, age, sex);

            // assert
            bmrResult.ShouldBe(expectedBMRResult);
        }

        [TestMethod]
        [DataRow(0f, 72.1f, 22, Sex.Male, "Weight, height, and age must be positive values.")]
        [DataRow(167.5f, -57.5f, 21, Sex.Female, "Weight, height, and age must be positive values.")]
        [DataRow(167.5f, -57.5f, -2, Sex.Female, "Weight, height, and age must be positive values.")]
        [DataRow(167.5f, 58.5f, 22, (Sex)99, "Invalid value for 'sex' parameter.")]
        public void CalculateBMR_InvalidData_ThrowArgumentException(float height, float weight, int age, Sex sex, string exceptionMessage)
        {
            // arrange

            // act
            Action act = () => Calculator.CalculateBMR(height, weight, age, sex);

            // assert
            var exception = Should.Throw<ArgumentException>(act);
            exception.Message.ShouldBe(exceptionMessage);
        }

        [TestMethod]
        public void CalculateBodyFat_ValidData_ShouldBeHigherForWideWaist()
        {
            // arrange
            bool result = false;

            // act
            float bfResult = Calculator.CalculateBodyFat(175.2f, 98.2f, 38.1f, 55.1f, Sex.Male);
            float bfResultWideWaist = Calculator.CalculateBodyFat(175.2f, 108.2f, 38.1f, 55.1f, Sex.Male);
            result = bfResultWideWaist > bfResult;

            // assert
            result.ShouldBeTrue();
        }

        [TestMethod]
        public void CalculateBodyFat_ValidData_ShouldBeLowerForTallerPerson()
        {
            // arrange
            bool result = false;

            // act
            float bfResult = Calculator.CalculateBodyFat(175.2f, 98.2f, 35.3f, 55.1f, Sex.Male);
            float bfResultTallerPerson = Calculator.CalculateBodyFat(185.2f, 98.2f, 38.3f, 55.1f, Sex.Male);
            result = bfResult > bfResultTallerPerson;

            // assert
            result.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow(0f, 72.1f, 55f, 23.2f, Sex.Male, "Height, waist, neck, and hip measurements must be positive values.")]
        [DataRow(167.5f, -57.5f, 21f, 24.2f, Sex.Male, "Height, waist, neck, and hip measurements must be positive values.")]
        [DataRow(167.5f, 57.5f, -2f, 22f, Sex.Female, "Height, waist, neck, and hip measurements must be positive values.")]
        [DataRow(167.5f, 57.5f, 42f, -22f, Sex.Female, "Height, waist, neck, and hip measurements must be positive values.")]
        [DataRow(167.5f, 0f, 42f, -22f, Sex.Female, "Height, waist, neck, and hip measurements must be positive values.")]
        [DataRow(167.5f, 58.5f, 22f, 33f, (Sex)99, "Invalid value for 'sex' parameter.")]
        public void CalculateBodyFat_InvalidData_ThrowArgumentException(float height, float waist, float neck, float hip, Sex sex, string exceptionMessage)
        {
            // arrange

            // act
            Action act = () => Calculator.CalculateBodyFat(height, waist, neck, hip, sex);

            // assert
            var exception = Should.Throw<ArgumentException>(act);
            exception.Message.ShouldBe(exceptionMessage);
        }
    }
}
