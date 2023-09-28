using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Validators.Trainings;
using Shouldly;

namespace FitnessPortalAPI.Tests.Validators.Trainings
{
    [TestClass]
    public class TrainingQueryValidatorTests
    {
        private readonly TrainingQueryValidator _trainingQueryValidator;
        private TrainingQuery _defaultTrainingQuery;
        public TrainingQueryValidatorTests()
        {
            _trainingQueryValidator = new TrainingQueryValidator();
            _defaultTrainingQuery = new TrainingQuery()
            {
                PageNumber = 1,
                PageSize = 10
            };
        }

        [TestMethod]
        public void TrainingQueryValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _trainingQueryValidator.Validate(_defaultTrainingQuery);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(0)]
        public void TrainingQueryValidator_ForbiddenPageNumberValue_ShouldBeInvalid(int pageSize)
        {
            // arrange
            _defaultTrainingQuery.PageNumber = pageSize;

            // act
            var testResult = _trainingQueryValidator.Validate(_defaultTrainingQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(20)]
        public void TrainingQueryValidator_PageSizeNotInAllowedSizes_ShouldBeInvalid(int pageSize)
        {
            // arrange
            _defaultTrainingQuery.PageSize = pageSize;

            // act
            var testResult = _trainingQueryValidator.Validate(_defaultTrainingQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
        }
    }
}
