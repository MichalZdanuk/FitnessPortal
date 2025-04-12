using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Models.UserProfileActions;

namespace FitnessPortalAPI.Validators.UserProfileActions
{
	public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly HashSet<string> commonPasswords;
        public RegisterUserDtoValidator(FitnessPortalDbContext dbContext)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string filePath = Path.Combine(currentDirectory, "DictionaryPasswords", "top_common_passwords.txt");

            commonPasswords = new HashSet<string>(File.ReadLines(filePath).Select(x => x.Trim()), StringComparer.OrdinalIgnoreCase);

            RuleFor(x => x.Email)
                .Length(2, 30)
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
                .Length(2, 30);

            //RuleFor(x => x.Password)
            //    .MinimumLength(3)
            //    .Must(NotBeCommonPassword).WithMessage("Password is too common. Please choose a stronger password.");
            /*.Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
            .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.");*/

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("'Password' must be same as 'ConfirmPassword'");

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
        private bool NotBeCommonPassword(string password)
        {
            return !commonPasswords.Contains(password);
        }

        private bool BeValidDateOfBirth(DateTime? dateOfBirth)
        {
            // Check if date of birth is not in the future and is on or after 1920.
            return dateOfBirth <= DateTime.Now && dateOfBirth?.Year >= 1920;
        }
    }
}
