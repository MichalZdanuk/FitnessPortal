using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Calculators
{
    public class CreateBodyFatQueryValidator : AbstractValidator<CreateBodyFatQuery>
    {
        private string[] genders = new[] { "male", "female" };
        public CreateBodyFatQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Waist)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Hip)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Neck)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Sex).Custom((value, context) =>
            {
                if (!genders.Contains(value.ToLower()))
                {
                    context.AddFailure("Sex", $"Sex must be in [{string.Join(",", genders)}]");
                }
            });
        }
    }
}
