using AutoMapper;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;
using TestSystem.Application.CQRS.Identity.Commands.RevokeAllTokens;
using TestSystem.Application.Interfaces;

namespace TestSystem.WebApi.Models.Identity
{
    public class RevokeAllTokensModel : IMapWith<RevokeAllTokensCommand>
    {
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RevokeAllTokensModel, RevokeAllTokensCommand>();
        }
    }
}
