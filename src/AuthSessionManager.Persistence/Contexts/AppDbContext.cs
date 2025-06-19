using Microsoft.EntityFrameworkCore;

namespace AuthSessionManager.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}