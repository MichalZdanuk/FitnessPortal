using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Articles;
using FluentValidation;

namespace FitnessPortalAPI.Validators.Articles
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator(FitnessPortalDbContext dbContext)
        {
            RuleFor(x => x.Title)
                .Length(5, 150);

            RuleFor(x => x.ShortDescription)
                .Length(10, 400);

            RuleFor(x => x.Content)
                .Length(10, 5000);

            RuleFor(x => x.Category)
                .Length(3, 30);
        }
    }
}
