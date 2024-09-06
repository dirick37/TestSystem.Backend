using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestSystem.Application.CQRS.Identity.Commands.RegisterUset
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? Role { get; set; }
    }
}
