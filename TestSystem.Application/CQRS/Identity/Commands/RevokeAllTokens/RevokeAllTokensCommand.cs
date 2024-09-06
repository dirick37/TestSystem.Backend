using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeAllTokens
{
    public class RevokeAllTokensCommand : IRequest<string>

    {
        public Guid UserId { get; set; }
    }
}
