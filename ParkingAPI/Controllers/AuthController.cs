using Common.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AuthService;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _config = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForAuthDto userForAuth)
        {
            string? token = _authService.Authenticate(userForAuth);
            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                status = "ok",
                msg = "Login successful",
                token
            });
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            string? token = _authService.Register(userForRegistration);
            if (token is null)
            {
                return BadRequest("Username not available");
            }

            return Ok(new
            {
                status = "ok",
                msg = "Register successful",
                token
            });
        }
    }
}