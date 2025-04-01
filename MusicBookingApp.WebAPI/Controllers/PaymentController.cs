using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enum;
using MusicBookingApp.Persistence.Services;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IFlutterwaveService _flutterwaveService;

        public PaymentController(IPaymentService paymentService, IMapper mapper, IFlutterwaveService flutterwaveService)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _flutterwaveService = flutterwaveService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
        {
            var paymentUrl = await _paymentService.ProcessPaymentAsync(request.BookingId, request.Amount, request.Email);
            return Ok(new ApiResponse<string>(true, 200, "Payment initiated successfully.", paymentUrl));
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetPaymentByBookingId(string bookingId)
        {
            var payment = await _paymentService.GetPaymentByBookingIdAsync(bookingId);
            if (payment == null)
                return NotFound(new ApiResponse<PaymentDto>(false, 404, "Payment not found.", null));

            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return Ok(new ApiResponse<PaymentDto>(true, 200, "Payment retrieved successfully.", paymentDto));
        }
        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] string transaction_id)
        {
            var isSuccessful = await _flutterwaveService.VerifyPaymentAsync(transaction_id);
            if (!isSuccessful)
                return BadRequest(new ApiResponse<bool>(false, 400, "Payment verification failed.", false));

            await _paymentService.UpdatePaymentStatusAsync(transaction_id, PaymentStatus.Completed);
            return Ok(new ApiResponse<bool>(true, 200, "Payment verified successfully.", true));
        }


        [HttpPut("{paymentId}")]
        public async Task<IActionResult> UpdatePaymentStatus(string paymentId, [FromBody] PaymentStatusUpdateDto statusUpdateDto)
        {
            var updated = await _paymentService.UpdatePaymentStatusAsync(paymentId, statusUpdateDto.PaymentStatus);
            if (!updated)
                return NotFound(new ApiResponse<bool>(false, 404, "Payment not found or status update failed.", false));

            return Ok(new ApiResponse<bool>(true, 200, "Payment status updated successfully.", true));
        }
    }
}
