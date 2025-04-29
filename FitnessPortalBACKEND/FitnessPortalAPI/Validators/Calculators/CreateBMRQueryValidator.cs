using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Validators.Calculators;
public class CreateBMRQueryValidator : AbstractValidator<CreateBMRQuery>
{
	public CreateBMRQueryValidator()
	{
		RuleFor(x => x.Height)
			.NotEmpty()
			.InclusiveBetween(80.0f, 240.0f);

		RuleFor(x => x.Weight)
			.NotEmpty()
			.InclusiveBetween(30.0f, 200.0f);

		RuleFor(x => x.Age)
			.NotEmpty()
			.InclusiveBetween(1, 100);

		RuleFor(x => x.Sex)
			.NotEmpty()
			.IsInEnum();
	}
}
