using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystem.Application.Common.Exceptions.Identity;
using TestSystem.Application.Interfaces;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeAllTokens
{
    public class RevokeAllTokensCommandHandler : IRequestHandler<RevokeAllTokensCommand, string>
    {
        private readonly ITestSystemDbContext _context;

        public RevokeAllTokensCommandHandler(ITestSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RevokeAllTokensCommand request, CancellationToken cancellationToken)
        {
            var tokens = await _context.RefreshTokens.ToListAsync();

            if (!tokens.Any())
            {
                throw new TokensNotFoundException();
            }

            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync(cancellationToken);

            return "All tokens have been revoked.";
        }
    }
}
