using FitnessPortalAPI.Models.Training;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Trainings
{
    public class ExerciseDtoValidator : AbstractValidator<ExerciseDto>
    {
        public ExerciseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.NumberOfReps)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Payload)
                .GreaterThanOrEqualTo(1);
        }
    }
}
