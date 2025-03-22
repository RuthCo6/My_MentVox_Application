using MentVox.Core.Models;
using MentVox.Data;
using MentVox_Application.Api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly ApplicationDbContext _context;
    public AuthController(JwtTokenGenerator jwtTokenGenerator, ApplicationDbContext context)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // כאן אפשר לשים לוגיקה אמיתית לבדוק את המשתמש בסיסמה
        if (model.Username == "rcMentVox" && model.Password == "654321")
        {
            var token = _jwtTokenGenerator.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }
        return Unauthorized();

        //var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        //if (user == null)
        //    return Unauthorized();

        //var token = _jwtTokenGenerator.GenerateToken(user.Username);
        //return Ok(new { Token = token });
    }


    [HttpPost("register")]
    public IActionResult Register([FromBody] LoginModel model)
    {
        if (_context.Users.Any(u => u.Username == model.Username))
            return BadRequest("Username already exists");

        var user = new User
        {
            Username = model.Username,
            Password = model.Password
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered");
    }
}
