using FitnessPortalAPI.Models.Calculators;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Calculators
{
    public class CreateBMIQueryValidator : AbstractValidator<CreateBMIQuery>
    {
        public CreateBMIQueryValidator()
        {
            RuleFor(x => x.Height)
                .NotEmpty()
                .InclusiveBetween(80.0f, 240.0f);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .InclusiveBetween(30.0f, 200.0f);
        }
    }
}
