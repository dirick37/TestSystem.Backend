using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSystem.Application.Common.Exceptions;
using TestSystem.Application.Common.Exceptions.Identity;
using TestSystem.Application.CQRS.DTOs;
using TestSystem.Application.Interfaces;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Application.CQRS.Identity.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, RefreshTokenDto>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ITestSystemDbContext _context;

        public LoginUserCommandHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ITestSystemDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<RefreshTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var isEmail = request.LoginOrEmail.Contains("@");
            var user = isEmail
                ? await _userManager.FindByEmailAsync(request.LoginOrEmail)
                : await _userManager.FindByNameAsync(request.LoginOrEmail);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

            if (!result.Succeeded)
            {
                throw new InvalidLoginException();
            }

            var token = await _tokenService.GenerateJwtTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync(cancellationToken);

            return new RefreshTokenDto(token, refreshToken);
        }
    }
}
