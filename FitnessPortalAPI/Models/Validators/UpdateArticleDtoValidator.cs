using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Articles;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators
{
    public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
    {
        public UpdateArticleDtoValidator(FitnessPortalDbContext dbContext)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.ShortDescription)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(5000);

            RuleFor(x => x.Category)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
