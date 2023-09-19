using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Validators.Trainings;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPortalAPI.Tests.Validators.Trainings
{
    [TestClass]
    public class CreateTrainingDtoValidatorTests
    {
        private readonly CreateTrainingDtoValidator _createTrainingDtoValidator;
        private CreateTrainingDto _defaultCreateTrainingDto;
        public CreateTrainingDtoValidatorTests()
        {
            _createTrainingDtoValidator = new CreateTrainingDtoValidator();
            _defaultCreateTrainingDto = new CreateTrainingDto()
            {
                NumberOfSeries = 5,
                Exercises = new List<CreateExerciseDto>()
                {
                    new CreateExerciseDto()
                    {
                        Name= "Ex_Name",
                        NumberOfReps = 5,
                        Payload = 24.5f
                    },
                    new CreateExerciseDto()
                    {
                        Name= "Ex_Name2",
                        NumberOfReps = 6,
                        Payload = 21.5f
                    },
                    new CreateExerciseDto()
                    {
                        Name= "Ex_Name3",
                        NumberOfReps = 10,
                        Payload = 14.5f
                    }
                }
            };
        }

        [TestMethod]
        public void CreateTrainingDtoValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createTrainingDtoValidator.Validate(_defaultCreateTrainingDto);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        public void CreateTrainingDtoValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            var createTrainingDtoEmpty = new CreateTrainingDto();

            // act
            var testResult = _createTrainingDtoValidator.Validate(createTrainingDtoEmpty);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(error => error.PropertyName == nameof(CreateTrainingDto.NumberOfSeries));
            testResult.Errors.ShouldContain(error => error.PropertyName == nameof(CreateTrainingDto.Exercises));

        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(20)]
        public void CreateTrainingDtoValidator_InvalidNumberOfSeriesValue_ShouldBeInvalid(int numberOfSeries)
        {
            // arrange
            _defaultCreateTrainingDto.NumberOfSeries = numberOfSeries;

            // act
            var testResult = _createTrainingDtoValidator.Validate(_defaultCreateTrainingDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count().ShouldBe(1);
            testResult.Errors.ShouldContain(error => error.PropertyName == nameof(CreateTrainingDto.NumberOfSeries));
        }

        [TestMethod]
        public void CreateTrainingDtoValidator_EmptyExercisesList_ShouldBeInvalid()
        {
            // arrange
            var createTrainingDto = new CreateTrainingDto()
            {
                NumberOfSeries = 5,
                Exercises = new List<CreateExerciseDto>()
            };

            // act
            var testResult = _createTrainingDtoValidator.Validate(createTrainingDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(error => error.PropertyName == nameof(CreateTrainingDto.Exercises));
        }

        [TestMethod]
        public void CreateTrainingDtoValidator_InvalidExerciseData_ShouldBeInvalid()
        {
            // arrange
            var createTrainingDto = new CreateTrainingDto()
            {
                NumberOfSeries = 5,
                Exercises = new List<CreateExerciseDto>()
                {
                    new CreateExerciseDto()
                    {
                        Name = "Valid_Name",
                        NumberOfReps = -1,
                        Payload = 24.5f
                    },
                    new CreateExerciseDto()
                    {
                        Name = "",
                        NumberOfReps = 6,
                        Payload = 21.5f
                    }
                }
            };

            // act
            var testResult = _createTrainingDtoValidator.Validate(createTrainingDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(2);
            testResult.Errors.ShouldContain(error => error.PropertyName == "Exercises[0].NumberOfReps");
            testResult.Errors.ShouldContain(error => error.PropertyName == "Exercises[1].Name");
        }
    }
}
