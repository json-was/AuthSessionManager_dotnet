namespace AuthSessionManager.Application.Common.Interfaces;

/// <summary>
/// Defines a contract for generating access tokens used in authentication workflows.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a short-lived access token (JWT) for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the authenticated user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A signed JSON Web Token (JWT) as a string.</returns>
    string GenerateAccessToken(Guid userId, string email);
}