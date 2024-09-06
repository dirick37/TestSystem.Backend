using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<string>
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
