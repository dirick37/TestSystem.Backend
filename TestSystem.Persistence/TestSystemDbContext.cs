using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestSystem.Application.Interfaces;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Persistence
{
    public class TestSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, ITestSystemDbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public TestSystemDbContext(DbContextOptions<TestSystemDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserToken<Guid>>();
        }
    }
}
