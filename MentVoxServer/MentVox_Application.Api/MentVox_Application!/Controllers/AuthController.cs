using MentVox_Application.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(JwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // כאן אפשר לשים לוגיקה אמיתית לבדוק את המשתמש בסיסמה
        if (model.Username == "testuser" && model.Password == "password")
        {
            var token = _jwtTokenGenerator.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
