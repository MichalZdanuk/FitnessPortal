using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models.Training;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.Trainings
{
    public class TrainingQueryValidator : AbstractValidator<TrainingQuery>
    {
        private int[] allowedPageSizes = new[] { 1, 3, 5, 10, 15 };
        public TrainingQueryValidator()
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
