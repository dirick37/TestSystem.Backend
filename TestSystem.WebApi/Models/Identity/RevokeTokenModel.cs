using AutoMapper;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;
using TestSystem.Application.CQRS.Identity.Commands.RevokeToken;
using TestSystem.Application.Interfaces;

namespace TestSystem.WebApi.Models.Identity
{
    public class RevokeTokenModel : IMapWith<RevokeTokenCommand>
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RevokeTokenModel, RevokeTokenCommand>();
        }
    }
}
