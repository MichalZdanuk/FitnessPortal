using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Calculators
{
    public class CreateBodyFatQueryValidator : AbstractValidator<CreateBodyFatQuery>
    {
        private string[] genders = new[] { "male", "female" };
        public CreateBodyFatQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .InclusiveBetween(80.0f, 240.0f);

            RuleFor(x => x.Waist)
                .NotEmpty()
                .InclusiveBetween(30.0f, 200.0f);

            RuleFor(x => x.Hip)
                .NotEmpty()
                .InclusiveBetween(30.0f, 200.0f);

            RuleFor(x => x.Neck)
                .NotEmpty()
                .InclusiveBetween(10.0f, 100.0f);

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
