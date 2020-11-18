using FluentValidation;
using Identity.Services.Interfaces.Models.User.Register;

namespace Identity.Services.Interfaces.Validation.FluentValidation.Register
{
    public class RegisterValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches("^(?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match.");
                }
            });

        }
    }
}
