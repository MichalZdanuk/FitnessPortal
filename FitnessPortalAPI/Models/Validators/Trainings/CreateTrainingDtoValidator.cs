using FitnessPortalAPI.Models.Training;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Trainings
{
    public class CreateTrainingDtoValidator : AbstractValidator<CreateTrainingDto>
    {
        public CreateTrainingDtoValidator()
        {
            RuleFor(x => x.NumberOfSeries)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.TotalPayload)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Exercises)
                .NotEmpty();
        }
    }
}
