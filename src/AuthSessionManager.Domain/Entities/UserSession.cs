namespace AuthSessionManager.Domain.Entities;

/// <summary>
/// Represents a single user session in the system, including metadata such as IP address, device,
/// token hash, and lifecycle timestamps. This entity supports tracking and revocation of refresh tokens.
/// </summary>
public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string RefreshTokenHash { get; private set; }
    public string IpAddress { get; private set; }
    public string DeviceInfo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime RevokedAt { get; private set; }

    private UserSession()
    {
    }

    /// <summary>
    /// Initializes a new user session with the specified user and client metadata.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="refreshTokenHash">Hashed refresh token.</param>
    /// <param name="ipAddress">Client IP address.</param>
    /// <param name="deviceInfo">Client device info or user agent.</param>
    public UserSession(Guid userId, string refreshTokenHash, string ipAddress, string deviceInfo)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RefreshTokenHash = refreshTokenHash;
        IpAddress = ipAddress;
        DeviceInfo = deviceInfo;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the session as revoked by setting the revocation timestamp.
    /// </summary>
    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Indicates whether the session is currently active (not revoked).
    /// </summary>
    public bool IsActive => RevokedAt == null;
}