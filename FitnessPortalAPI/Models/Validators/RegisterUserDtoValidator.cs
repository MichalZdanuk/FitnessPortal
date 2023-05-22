using FitnessPortalAPI.Entities;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(FitnessPortalDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email cannot be empty");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty");

            RuleFor(x => x.Username)
                .Length(2, 30)
                .WithMessage("Incorrect username length");

            RuleFor(x => x.Password)
                .MinimumLength(3).WithMessage("Minimum length of password is 3");
                /*.Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.");*/

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("'Password' must be same as 'ConfirmPassword'");

            
        }
    }
}
