using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Calculators
{
    public class CreateBMRQueryValidator : AbstractValidator<CreateBMRQuery>
    {
        private string[] genders = new[] { "male", "female" };
        public CreateBMRQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Age)
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
