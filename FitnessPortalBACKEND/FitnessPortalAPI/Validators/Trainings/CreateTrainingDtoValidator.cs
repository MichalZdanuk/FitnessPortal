using FitnessPortalAPI.Models.Trainings;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Trainings
{
    public class CreateTrainingDtoValidator : AbstractValidator<CreateTrainingDto>
    {
        public CreateTrainingDtoValidator()
        {
            RuleFor(x => x.NumberOfSeries)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.TotalPayload)
                .InclusiveBetween(1.0f, 100_000f);

            RuleFor(x => x.Exercises)
                .NotEmpty();
        }
    }
}
