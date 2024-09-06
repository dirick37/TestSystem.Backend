using MediatR;
using Microsoft.EntityFrameworkCore;
using TestSystem.Application.Common.Exceptions.Identity;
using TestSystem.Application.Interfaces;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeAllUsers
{
    public class RevokeAllUsersCommandHandler : IRequestHandler<RevokeAllUsersCommand, string>
    {
        private readonly ITestSystemDbContext _context;
        public RevokeAllUsersCommandHandler(ITestSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RevokeAllUsersCommand request, CancellationToken cancellationToken)
        {
            var tokens = await _context.RefreshTokens.ToListAsync();

            if (!tokens.Any())
            {
                throw new TokensNotFoundException();
            }

            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync(cancellationToken);

            return "All Users have been revoked.";
        }
    }
}
