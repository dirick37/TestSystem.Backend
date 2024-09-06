using Microsoft.EntityFrameworkCore;
using TestSystem.Domain.Data.Entities;

namespace TestSystem.Application.Interfaces
{
    public interface ITestSystemDbContext
    {
        DbSet<Administrator> Administrators { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
