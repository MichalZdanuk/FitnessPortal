using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Validators.UserProfileActions;
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
