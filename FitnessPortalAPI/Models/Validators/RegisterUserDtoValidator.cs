using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.UserActions;
using FluentValidation;

namespace FitnessPortalAPI.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        private readonly HashSet<string> commonPasswords;
        public RegisterUserDtoValidator(FitnessPortalDbContext dbContext)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string filePath = Path.Combine(currentDirectory, "DictionaryPasswords", "top_common_passwords.txt");

            commonPasswords = new HashSet<string>(File.ReadLines(filePath).Select(x => x.Trim()), StringComparer.OrdinalIgnoreCase);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

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
                .Length(2, 30);

            RuleFor(x => x.Password)
                .MinimumLength(3)
                .Must(NotBeCommonPassword).WithMessage("Password is too common. Please choose a stronger password.");
                /*.Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.");*/

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("'Password' must be same as 'ConfirmPassword'");

            
        }
        private bool NotBeCommonPassword(string password)
        {
            return !commonPasswords.Contains(password);
        }
    }
}
