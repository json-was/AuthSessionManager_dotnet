using AuthSessionManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthSessionManager.Persistence.Contexts;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// Defines the mappings between domain entities and database tables.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with the given options.
    /// </summary>
    /// <param name="options">Options to configure the context, including connection string and provider.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
}