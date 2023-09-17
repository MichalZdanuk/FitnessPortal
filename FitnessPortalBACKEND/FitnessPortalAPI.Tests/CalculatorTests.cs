using FitnessPortalAPI.Constants;
using Shouldly;

namespace FitnessPortalAPI.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _calculator;
        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

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
            _calculator.CalculateBMI(height, weight, out bmiIndex, out bmiCategory);

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
            Action act = () => _calculator.CalculateBMI(height, weight, out _, out _);

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
            Action act = () => _calculator.CalculateBMI(height, weight, out _, out _);

            // assert
            var exception = Should.Throw<ArgumentException>(act);
            exception.Message.ShouldBe("Height and weight values are out of valid range.");
        }
    }
}
