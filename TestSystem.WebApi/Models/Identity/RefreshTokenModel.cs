using AutoMapper;
using TestSystem.Application.CQRS.DTOs;
using TestSystem.Application.CQRS.Identity.Commands.UpdateRefreshToken;
using TestSystem.Application.Interfaces;

namespace TestSystem.WebApi.Models.Identity
{
    public class RefreshTokenModel : IMapWith<RefreshTokenDto>, IMapWith<UpdateRefreshTokenCommand>
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshTokenDto, RefreshTokenModel>();
            profile.CreateMap<RefreshTokenModel, UpdateRefreshTokenCommand>();
        }
    }
}
