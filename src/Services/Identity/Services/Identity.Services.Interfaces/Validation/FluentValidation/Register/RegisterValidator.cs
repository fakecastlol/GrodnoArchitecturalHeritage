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
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches("^(?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("Incorrect format. Minimum length 8, at least one uppercase letter, one lowercase letter, one digit and one character.");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.");

            RuleFor(user => user.ConfirmPassword)
                .Equal(user => user.Password).WithMessage("Passwords should match.");
        }
    }
}