using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Application.Validators;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;

        public UserController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<RegisterUserDto>(false, 400, "Validation failed.", null));
            }

            var user = await _applicationUserService.CreateUserAsync(userDto);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new ApiResponse<ApplicationUser>(true, 201, "User created successfully.", user));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _applicationUserService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<ApplicationUser>(false, 404, "User not found.", null));

            return Ok(new ApiResponse<ApplicationUser>(true, 200, "User retrieved successfully.", user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UpdateUserProfileDto dto)
        {
            var validationResult = await new UpdateUserProfileDtoValidator().ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<UpdateUserProfileDto>(false, 400, "Validation failed.", null));
            }

            var updatedUser = await _applicationUserService.UpdateUserProfileAsync(id, dto.FirstName, dto.LastName);
            return Ok(new ApiResponse<ApplicationUser>(true, 200, "User profile updated successfully.", updatedUser));
        }

        [HttpGet("{id}/bookings")]
        public async Task<IActionResult> GetUserBookings(string id)
        {
            var bookings = await _applicationUserService.GetUserBookingsAsync(id);
            return Ok(new ApiResponse<ICollection<Booking>>(true, 200, "Bookings retrieved successfully.", bookings));
        }
    }
}
