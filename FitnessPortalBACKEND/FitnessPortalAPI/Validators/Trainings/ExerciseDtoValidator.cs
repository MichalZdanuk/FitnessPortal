using FitnessPortalAPI.Models.Trainings;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Trainings
{
    public class ExerciseDtoValidator : AbstractValidator<ExerciseDto>
    {
        public ExerciseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.NumberOfReps)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.Payload)
                .InclusiveBetween(1.0f, 400f);
        }
    }
}
