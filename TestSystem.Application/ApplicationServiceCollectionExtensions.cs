using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestSystem.Application.Common.Behaviors;
using TestSystem.Application.Common.Mappings;
using TestSystem.Application.Interfaces;
using TestSystem.Application.Services;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITokenService, TokenService>(sp =>
            {
                var jwtSecret = jwtSettings["Secret"];
                var issuer = jwtSettings["Issuer"];
                var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

                if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(issuer))
                {
                    throw new InvalidOperationException("JWT settings are not configured properly.");
                }
                return new TokenService(jwtSecret, issuer, userManager);
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
