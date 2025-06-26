using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthSessionManager.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AuthSessionManager.Infrastructure.Services;

/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWT) for authentication.
/// </summary>
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;

    /// <summary>
    /// Constructs the TokenService, initializing the symmetric security key
    /// using the signing key from configuration settings.
    /// </summary>
    /// <param name="configuration">Application configuration to access JWT settings.</param>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;

        // Retrieve the signing key from configuration and create a symmetric security key
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));
    }

    /// <summary>
    /// Generates a JWT access token containing user claims.
    /// </summary>
    /// <param name="userId">Unique identifier of the user.</param>
    /// <param name="email">User's email address.</param>
    /// <returns>A signed JWT as a string.</returns>
    public string GenerateAccessToken(Guid userId, string email)
    {
        // Define the claims that will be embedded in the token payload
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Subject (user ID)
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.GivenName, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token identifier
        };

        // Create signing credentials using the symmetric key and HMAC-SHA512 algorithm
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        // Describe the token parameters including claims, expiration, issuer, and audience
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(15), // Token valid for 15 minutes
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        // Create the JWT token using the descriptor
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Serialize the token to a compact string format
        return tokenHandler.WriteToken(token);
    }
}