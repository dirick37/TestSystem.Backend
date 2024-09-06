using FluentValidation;

namespace TestSystem.Application.CQRS.Identity.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginOrEmail)
                .NotEmpty().WithMessage("UserName or Email is required.")
                .Length(3, 50).WithMessage("UserName or Email must be between 3 and 50 characters")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("UserName can only contain letters, numbers, and underscores");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 50).WithMessage("Password must be between 6 and 50 characters");
        }
    }
}
