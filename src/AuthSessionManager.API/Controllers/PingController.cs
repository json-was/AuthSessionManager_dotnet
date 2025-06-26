using AuthSessionManager.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSessionManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("pong");
    }

    [HttpGet("test-db")]
    public async Task<IActionResult> TestDb([FromServices] AppDbContext db)
    {
        try
        {
            await db.Database.EnsureCreatedAsync();
            return Ok("Database connection works!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}