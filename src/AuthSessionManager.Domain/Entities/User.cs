namespace AuthSessionManager.Domain.Entities;

/// <summary>
/// Represents an authenticated user in the system, including credentials and creation metadata.
/// </summary>
public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private User()
    {
    }

    /// <summary>
    /// Initializes a new user entity with the specified identity and credentials.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="email">Email address of the user.</param>
    /// <param name="passwordHash">Hashed password for secure authentication.</param>
    /// <param name="createdAt">Creation date of the user account (ignored in favor of UTC).</param>
    public User(Guid id, string email, string passwordHash, DateTime createdAt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }
}