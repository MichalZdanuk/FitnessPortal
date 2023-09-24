using FitnessPortalAPI.Models.Trainings;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Trainings
{
    public class CreateExerciseDtoValidator : AbstractValidator<CreateExerciseDto>
    {
        public CreateExerciseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.NumberOfReps)
                .NotEmpty()
                .InclusiveBetween(1, 100);

            RuleFor(x => x.Payload)
                .NotEmpty()
                .InclusiveBetween(0.5f, 200f);
        }
    }
}
