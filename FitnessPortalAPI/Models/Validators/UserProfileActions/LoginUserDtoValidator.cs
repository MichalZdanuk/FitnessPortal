using FitnessPortalAPI.Models.UserActions;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators.UserProfileActions
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
