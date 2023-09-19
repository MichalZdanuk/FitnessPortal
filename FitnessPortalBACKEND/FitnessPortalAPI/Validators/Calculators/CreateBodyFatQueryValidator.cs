using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Calculators
{
    public class CreateBodyFatQueryValidator : AbstractValidator<CreateBodyFatQuery>
    {
        public CreateBodyFatQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .InclusiveBetween(80.0f, 240.0f);

            RuleFor(x => x.Waist)
                .NotEmpty()
                .InclusiveBetween(30.0f, 150.0f);

            RuleFor(x => x.Hip)
                .NotEmpty()
                .InclusiveBetween(30.0f, 100.0f);

            RuleFor(x => x.Neck)
                .NotEmpty()
                .InclusiveBetween(10.0f, 70.0f);

            RuleFor(x => x.Sex)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
