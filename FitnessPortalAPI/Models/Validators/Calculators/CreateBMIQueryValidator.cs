using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Calculators
{
    public class CreateBMIQueryValidator : AbstractValidator<CreateBMIQuery>
    {
        public CreateBMIQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
