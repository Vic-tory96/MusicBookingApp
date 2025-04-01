using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(string userId)
        {
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(new ApiResponse<IEnumerable<BookingDto>>(true, 200, "Bookings retrieved successfully.", bookingDtos));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
        {
            var booking = await _bookingService.CreateBookingAsync(bookingDto.UserId, bookingDto.EventId);
            if (booking == null)
                return BadRequest(new ApiResponse<BookingDto>(false, 400, "Booking failed. User may have already booked this event.", null));

            var bookingDtoResponse = _mapper.Map<BookingDto>(booking);
            return Ok(new ApiResponse<BookingDto>(true, 201, "Booking created successfully.", bookingDtoResponse));
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(string bookingId, [FromBody] BookingDto bookingDto)
        {
            // Map the incoming BookingDto to the Booking entity
            var updatedBooking = _mapper.Map<Booking>(bookingDto);

            var updatedBookingEntity = await _bookingService.UpdateBookingAsync(bookingId, updatedBooking);

            if (updatedBookingEntity == null)
                return NotFound(new ApiResponse<BookingDto>(false, 404, "Booking not found.", null));

            // Map the updated entity to the BookingDto for the response
            var updatedBookingDto = _mapper.Map<BookingDto>(updatedBookingEntity);

            return Ok(new ApiResponse<BookingDto>(true, 200, "Booking updated successfully.", updatedBookingDto));
        }


        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(string bookingId)
        {
            var deleted = await _bookingService.DeleteBookingAsync(bookingId);
            if (!deleted)
                return NotFound(new ApiResponse<bool>(false, 404, "Booking not found.", false));

            return Ok(new ApiResponse<bool>(true, 200, "Booking deleted successfully.", true));
        }
    }
}
