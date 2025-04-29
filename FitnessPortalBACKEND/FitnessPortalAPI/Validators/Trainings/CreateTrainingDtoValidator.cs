using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Validators.Trainings;
public class CreateTrainingDtoValidator : AbstractValidator<CreateTrainingDto>
{
	public CreateTrainingDtoValidator()
	{
		RuleFor(x => x.NumberOfSeries)
			.NotEmpty()
			.InclusiveBetween(1, 12);

		RuleFor(x => x.Exercises)
			.NotEmpty()
			.ForEach(exercise => exercise.SetValidator(new CreateExerciseDtoValidator()));
	}
}
