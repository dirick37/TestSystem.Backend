using AutoMapper;
using TestSystem.Application.CQRS.Identity.Commands.LoginUser;
using TestSystem.Application.Interfaces;

namespace TestSystem.WebApi.Models.Identity
{
    public class LoginModel : IMapWith<LoginUserCommand>
    {
        public string LoginOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginModel, LoginUserCommand>();
        }
    }
}
