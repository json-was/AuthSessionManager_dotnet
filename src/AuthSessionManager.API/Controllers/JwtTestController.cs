using AuthSessionManager.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthSessionManager.API.Controllers;

/// <summary>
/// Provides a basic test endpoint for issuing a JWT access token.
/// </summary>
[ApiController]
[Route("api/test")]
public class JwtTestController : ControllerBase
{
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTestController"/> class.
    /// </summary>
    /// <param name="tokenService">The service responsible for generating JWT tokens.</param>
    public JwtTestController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Issues a test JWT token with test user information.
    /// </summary>
    /// <returns>A JSON object containing the generated JWT token.</returns>
    [HttpGet("token")]
    public IActionResult GetToken()
    {
        var token = _tokenService.GenerateAccessToken(Guid.NewGuid(), "test@example.com");
        return Ok(new { token });
    }
}