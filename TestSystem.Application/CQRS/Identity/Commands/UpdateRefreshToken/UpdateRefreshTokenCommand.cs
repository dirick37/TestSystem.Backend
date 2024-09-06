using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Application.CQRS.DTOs;

namespace TestSystem.Application.CQRS.Identity.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommand : IRequest<RefreshTokenDto>
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
