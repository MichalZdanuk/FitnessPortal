using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI.Validators.Articles;
public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
{
	public UpdateArticleDtoValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.Length(5, 150);

		RuleFor(x => x.ShortDescription)
			.NotEmpty()
			.Length(10, 400);

		RuleFor(x => x.Content)
			.NotEmpty()
			.Length(10, 5000);

		RuleFor(x => x.Category)
			.NotEmpty()
			.Length(3, 30);
	}
}
