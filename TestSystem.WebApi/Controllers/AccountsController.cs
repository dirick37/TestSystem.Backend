using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;
using TestSystem.Application.CQRS.Identity.Commands.RevokeAllTokens;
using TestSystem.Application.CQRS.Identity.Commands.RevokeToken;
using TestSystem.Domain.Data.Models;
using TestSystem.Application.CQRS.Identity.Commands.UpdateRefreshToken;
using TestSystem.Application.CQRS.Identity.Commands.RevokeAllUsers;
using TestSystem.WebApi.Models.Identity;
using AutoMapper;
using TestSystem.Application.CQRS.Identity.Commands.LoginUser;

namespace TestSystem.WebApi.Controllers
{
    [ApiController]
    [Route("TestSystemServices/v1/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var command = _mapper.Map<RegisterUserCommand>(registerModel);
            var result = await _mediator.Send(command);
            return Ok(new { value = result });
        }

        [HttpPost("login")]
        public async Task<ActionResult<RefreshTokenModel>> Login([FromBody] LoginModel loginModel)
        {
            var command = _mapper.Map<LoginUserCommand>(loginModel);
            var refresshToken = await _mediator.Send(command);
            return Ok(refresshToken);
        }

        
        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenModel>> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
        {
            var command = _mapper.Map<UpdateRefreshTokenCommand>(refreshTokenModel);
            var refresshToken = await _mediator.Send(command);
            return Ok(refresshToken);
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> Revoke([FromBody] RevokeTokenModel revokeTokenModel)
        {
            var command = _mapper.Map<RevokeTokenCommand>(revokeTokenModel);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("revoke-all-tokens")]
        public async Task<IActionResult> RevokeAllTokens([FromBody] RevokeAllTokensModel revokeAllTokensModel)
        {
            var command = _mapper.Map<RevokeAllTokensCommand>(revokeAllTokensModel);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("revoke-all-users")]
        public async Task<IActionResult> RevokeAll()
        {
            var command = new RevokeAllUsersCommand();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
