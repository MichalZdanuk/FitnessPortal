using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Validators.Trainings;
using Shouldly;

namespace FitnessPortalAPI.Tests.Validators.Trainings
{
    [TestClass]
    public class CreateExerciseDtoValidatorTests
    {
        private readonly CreateExerciseDtoValidator _createExerciseDtoValidator;
        private CreateExerciseDto _defaultCreateExerciseDto;
        public CreateExerciseDtoValidatorTests()
        {
            _createExerciseDtoValidator = new CreateExerciseDtoValidator();
            _defaultCreateExerciseDto = new CreateExerciseDto()
            {
                Name = "Ex_Name",
                NumberOfReps = 5,
                Payload = 50.5f
            };
        }

        [TestMethod]
        public void CreateExerciseDtoValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createExerciseDtoValidator.Validate(_defaultCreateExerciseDto);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        public void CreateExerciseDtoValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            var createExerciseDto = new CreateExerciseDto();

            // act
            var testResult = _createExerciseDtoValidator.Validate(createExerciseDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.Name));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.NumberOfReps));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.Payload));
        }

        [TestMethod]
        [DataRow("Name")]
        public void CreateExerciseDtoValidator_InvalidLengthName_ShouldBeInvalid(string name)
        {
            // arrange
            name += new string('X', 50);
            _defaultCreateExerciseDto.Name = name;

            // act
            var testResult = _createExerciseDtoValidator.Validate(_defaultCreateExerciseDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.Name));
        }

        [TestMethod]
        [DataRow(-5)]
        [DataRow(103)]
        public void CreateExerciseDtoValidator_NumberOfRepsValueNotInRange_ShouldBeInvalid(int numberOfReps)
        {
            // arrange
            _defaultCreateExerciseDto.NumberOfReps = numberOfReps;

            // act
            var testResult = _createExerciseDtoValidator.Validate(_defaultCreateExerciseDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.NumberOfReps));
        }

        [TestMethod]
        [DataRow(0.1f)]
        [DataRow(500f)]
        public void CreateExerciseDtoValidator_PayloadValueNotInRange_ShouldBeInvalid(float payload)
        {
            // arrange
            _defaultCreateExerciseDto.Payload = payload;

            // act
            var testResult = _createExerciseDtoValidator.Validate(_defaultCreateExerciseDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateExerciseDto.Payload));
        }
    }
}
