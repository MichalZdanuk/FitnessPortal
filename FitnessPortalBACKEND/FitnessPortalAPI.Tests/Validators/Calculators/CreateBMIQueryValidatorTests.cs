using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Validators.Calculators;
using Shouldly;

namespace FitnessPortalAPI.Tests.Validators.Calculators
{
    [TestClass]
    public class CreateBMIQueryValidatorTests
    {
        private readonly CreateBMIQueryValidator _createBMIQueryValidator;
        private CreateBMIQuery _defaultCreateBMIQuery;
        public CreateBMIQueryValidatorTests()
        {
            _createBMIQueryValidator = new CreateBMIQueryValidator();
            _defaultCreateBMIQuery = new CreateBMIQuery()
            {
                Height = 172f,
                Weight = 71.5f
            };
        }

        [TestMethod]
        public void CreateBMIQueryValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createBMIQueryValidator.Validate(_defaultCreateBMIQuery);

            // assert
            testResult.IsValid.ShouldBeTrue();
        }

        [TestMethod]
        public void CreateBMIQueryValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            var queryWithMissingData = new CreateBMIQuery() { };

            // act
            var testResult = _createBMIQueryValidator.Validate(queryWithMissingData);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMIQuery.Height));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMIQuery.Weight));
        }

        [TestMethod]
        [DataRow(75f)]
        [DataRow(241f)]
        public void CreateBMIQueryValidator_HeightValueNotInRange_ShouldBeInvalid(float height)
        {
            // arrange
            _defaultCreateBMIQuery.Height = height;

            // act
            var testResult = _createBMIQueryValidator.Validate(_defaultCreateBMIQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMIQuery.Height));
        }

        [TestMethod]
        [DataRow(29.5f)]
        [DataRow(200.1f)]
        public void CreateBMIQueryValidator_WeightValueNotInRange_ShouldBeInvalid(float weight)
        {
            // arrange
            _defaultCreateBMIQuery.Weight = weight;

            // act
            var testResult = _createBMIQueryValidator.Validate(_defaultCreateBMIQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMIQuery.Weight));
        }
    }
}
