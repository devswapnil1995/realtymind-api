using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.Services.Auth;
using RealtyMind.Infrastructure.Data;
using RealtyMind.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/auth")] 
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenService _tokenService;

        public AuthController(AppDbContext db, JwtTokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("User already exists");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "Buyer"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return BadRequest("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Invalid email or password");

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
    public record RegisterRequest(string Email, string Password);
    public record LoginRequest(string Email, string Password);
}
