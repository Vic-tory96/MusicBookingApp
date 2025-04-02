
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Authentication;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IJwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new ApiResponse<string>(false, 401, "Invalid credentials", null));
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new ApiResponse<string>(true, 200, "Login successful", token));
        }
    }
}
