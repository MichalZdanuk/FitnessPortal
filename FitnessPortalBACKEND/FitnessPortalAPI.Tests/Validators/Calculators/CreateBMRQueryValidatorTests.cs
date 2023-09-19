using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Validators.Calculators;
using Shouldly;

namespace FitnessPortalAPI.Tests.Validators.Calculators
{
    [TestClass]
    public class CreateBMRQueryValidatorTests
    {
        private readonly CreateBMRQueryValidator _createBMRQueryValidator;
        private CreateBMRQuery _defaultCreateBMRQuery;
        public CreateBMRQueryValidatorTests()
        {
            _createBMRQueryValidator = new CreateBMRQueryValidator();
            _defaultCreateBMRQuery = new CreateBMRQuery()
            {
                Sex = Sex.Male,
                Height = 172.5f,
                Weight = 56.5f,
                Age = 22
            };
        }

        [TestMethod]
        public void CreateBMRQueryValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createBMRQueryValidator.Validate(_defaultCreateBMRQuery);

            // assert
            testResult.IsValid.ShouldBeTrue();
        }

        [TestMethod]
        public void CreateBMRQueryValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            var queryWithMissingData = new CreateBMRQuery() { };

            // act
            var testResult = _createBMRQueryValidator.Validate(queryWithMissingData);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Height));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Weight));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Sex));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Age));
        }

        [TestMethod]
        [DataRow(75f)]
        [DataRow(241f)]
        public void CreateBMRQueryValidator_HeightValueNotInRange_ShouldBeInvalid(float height)
        {
            // arrange
            _defaultCreateBMRQuery.Height = height;

            // act
            var testResult = _createBMRQueryValidator.Validate(_defaultCreateBMRQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Height));
        }

        [TestMethod]
        [DataRow(29.5f)]
        [DataRow(200.1f)]
        public void CreateBMRQueryValidator_WeightValueNotInRange_ShouldBeInvalid(float weight)
        {
            // arrange
            _defaultCreateBMRQuery.Weight = weight;

            // act
            var testResult = _createBMRQueryValidator.Validate(_defaultCreateBMRQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Weight));
        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(101)]
        public void CreateBMRQueryValidator_AgeValueNotInRange_ShouldBeInvalid(int age)
        {
            // arrange
            _defaultCreateBMRQuery.Age = age;

            // act
            var testResult = _createBMRQueryValidator.Validate(_defaultCreateBMRQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Age));
        }

        [TestMethod]
        [DataRow((Sex)99)]
        public void CreateBMRQueryValidator_InvalidSexValue_ShouldBeInvalid(Sex sex)
        {
            // arrange
            _defaultCreateBMRQuery.Sex = sex;

            // act
            var testResult = _createBMRQueryValidator.Validate(_defaultCreateBMRQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBMRQuery.Sex));
        }
    }
}
