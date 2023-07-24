using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.UserActions;
using FluentValidation;
using System.Security.Claims;

namespace FitnessPortalAPI.Models.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        
        public UpdateUserDtoValidator(FitnessPortalDbContext dbContext, IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;

            RuleFor(x => x.Email)
                .NotEmpty()
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
                .NotEmpty();

            RuleFor(x => x.Username)
                .Length(2, 30);
        }
    }
}
