using FluentValidation;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;

namespace TestSystem.Application.CQRS.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .Length(3, 50).WithMessage("UserName must be between 3 and 50 characters")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("UserName can only contain letters, numbers, and underscores");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Length(3, 50).WithMessage("Email must be between 3 and 50 characters")
                .EmailAddress().WithMessage("A valid email is required.");

                RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(6, 50).WithMessage("Password must be between 6 and 50 characters")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
                .Matches(@"^\S*$").WithMessage("Password cannot contain spaces");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(3, 50).WithMessage("First name must be between 3 and 50 characters")
                .Matches("^[А-Яа-яЁё]+$").WithMessage("First name must contain only Russian letters without spaces or special characters.");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters")
                .Matches("^[А-Яа-яЁё]+$").WithMessage("Last name must contain only Russian letters without spaces or special characters.");

            RuleFor(x => x.MiddleName)
                .Length(3, 50).WithMessage("Middle name must be between 3 and 50 characters")
                .Matches("^[А-Яа-яЁё]+$").WithMessage("Middle name must contain only Russian letters without spaces or special characters.")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.Role)
                .Must(role => role == null || new[] { "Teacher", "Student" }.Contains(role))
                .WithMessage("Role must be either Teacher or Student.");
        }
    }
}
