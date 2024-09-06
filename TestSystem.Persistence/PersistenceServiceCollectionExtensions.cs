using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestSystem.Application.Interfaces;

namespace TestSystem.Persistence
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
                   IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ITestSystemDbContext, TestSystemDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}
