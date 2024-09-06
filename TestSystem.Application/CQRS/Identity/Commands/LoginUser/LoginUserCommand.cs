using MediatR;
using TestSystem.Application.CQRS.DTOs;

namespace TestSystem.Application.CQRS.Identity.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<RefreshTokenDto>
    {
        public string LoginOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
