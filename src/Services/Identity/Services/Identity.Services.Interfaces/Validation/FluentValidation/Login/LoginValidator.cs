using FluentValidation;
using Identity.Services.Interfaces.Models.User.Login;

namespace Identity.Services.Interfaces.Validation.FluentValidation.Login
{
    public class LoginValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
