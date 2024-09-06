using Microsoft.AspNetCore.Identity;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Persistence
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Administrator", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole<Guid> { Name = role, NormalizedName = role.ToUpper() };
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
