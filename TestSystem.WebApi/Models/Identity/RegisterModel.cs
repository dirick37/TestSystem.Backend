using AutoMapper;
using TestSystem.Application.CQRS.Identity.Commands.RegisterUset;
using TestSystem.Application.Interfaces;

namespace TestSystem.WebApi.Models.Identity
{
    public class RegisterModel : IMapWith<RegisterUserCommand>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterModel, RegisterUserCommand>();
        }
    }
}
