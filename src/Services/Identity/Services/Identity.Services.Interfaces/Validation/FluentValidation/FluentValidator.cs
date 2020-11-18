using FluentValidation;
using Identity.Services.Interfaces.Models.User.Register;

namespace Identity.Services.Interfaces.Validation.FluentValidation
{
    public class FluentValidator : AbstractValidator<RegisterRequestModel>
    {
        public FluentValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .Matches("^(?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$");
        }
    }
}
