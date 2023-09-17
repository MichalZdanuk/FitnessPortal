using FitnessPortalAPI.Models.Articles;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Articles
{
    public class ArticleQueryValidator : AbstractValidator<ArticleQuery>
    {
        private int[] allowedPageSizes = new[] { 3, 5, 10 };
        public ArticleQueryValidator()
        {
            RuleFor(a => a.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1");
            RuleFor(a => a.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
