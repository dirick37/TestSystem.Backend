using MediatR;
using Microsoft.EntityFrameworkCore;
using TestSystem.Application.Common.Exceptions.Identity;
using TestSystem.Application.Interfaces;

namespace TestSystem.Application.CQRS.Identity.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, string>
    {
        private readonly ITestSystemDbContext _context;

        public RevokeTokenCommandHandler(ITestSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var savedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == request.UserId);

            if (savedRefreshToken == null)
            {
                throw new InvalidOrExpiredRefreshTokenException();
            }

            _context.RefreshTokens.Remove(savedRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return "Logged out successfully.";
        }
    }
}
