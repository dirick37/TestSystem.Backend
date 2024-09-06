using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestSystem.Application.Common.Exceptions.Identity;
using TestSystem.Application.CQRS.DTOs;
using TestSystem.Application.Interfaces;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Application.CQRS.Identity.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, RefreshTokenDto>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITestSystemDbContext _context;

        public UpdateRefreshTokenCommandHandler(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            ITestSystemDbContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<RefreshTokenDto> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new UserNotFoundException();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserIdNotFoundException(userId);
            }

            var savedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == user.Id);

            if (savedRefreshToken == null || savedRefreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                throw new InvalidOrExpiredRefreshTokenException();
            }

            var newAccessToken = await _tokenService.GenerateJwtTokenAsync(user);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            _context.RefreshTokens.Remove(savedRefreshToken);
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync(cancellationToken);

            return new RefreshTokenDto(newAccessToken, newRefreshToken);
        }
    }
}
