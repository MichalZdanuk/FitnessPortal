using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Models.UserProfileActions;
using System.Security.Claims;

namespace FitnessPortalAPI.Validators.UserProfileActions
{
	public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UpdateUserDtoValidator(FitnessPortalDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

            RuleFor(x => x.Email)
                .Length(2, 30)
                .EmailAddress();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                    var emailInUse = dbContext.Users
                    .Where(u => u.Id != userId)
                    .Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(x => x.Username)
                .Length(2, 30);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Must(BeValidDateOfBirth).WithMessage("Date of birth is not valid.");

            RuleFor(x => x.Weight)
                .NotEmpty()
                .InclusiveBetween(30.0f, 200.0f);

            RuleFor(x => x.Height)
                .NotEmpty()
                .InclusiveBetween(80.0f, 240.0f);
        }

        private bool BeValidDateOfBirth(DateTime? dateOfBirth)
        {
            // Check if date of birth is not in the future and is on or after 1920.
            return dateOfBirth <= DateTime.Now && dateOfBirth?.Year >= 1920;
        }
    }
}
