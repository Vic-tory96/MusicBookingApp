using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enum;

namespace MusicBookingApp.Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlutterwaveService _flutterwaveService;
        public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository, IFlutterwaveService flutterwaveService)
        {
            _bookingRepository = bookingRepository;
            _paymentRepository = paymentRepository;
            _flutterwaveService = flutterwaveService;
        }
        public async Task<Payment?> GetPaymentByBookingIdAsync(string bookingId)
        {
            return await _paymentRepository.GetPaymentByBookingIdAsync(bookingId);
        }

        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _paymentRepository.GetPaymentByTransactionIdAsync(transactionId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _paymentRepository.GetPaymentsByUserIdAsync(userId);
        }

        public async Task<string> ProcessPaymentAsync(string bookingId, decimal amount, string email)
        {
            var existingPayment = await _paymentRepository.GetPaymentByBookingIdAsync(bookingId);
            if (existingPayment != null)
            {
                throw new Exception("A payment has already been initiated for this booking.");
            }

            var transactionRef = Guid.NewGuid().ToString();

            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            var paymentLink = await _flutterwaveService.InitializePaymentAsync(amount, email, transactionRef);
            if (string.IsNullOrEmpty(paymentLink))
            {
                throw new Exception("Failed to generate payment link.");
            }

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentStatus = PaymentStatus.Pending,
                TransactionId = transactionRef,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.CreatePaymentAsync(payment);

            // Update the Booking with PaymentId
            var getBooking = await _bookingRepository.GetByIdAsync(bookingId);
            if (getBooking != null)
            {
                getBooking.PaymentId = payment.Id;
                await _bookingRepository.UpdateAsync(booking);
            }

            return paymentLink;
        }
       
        public async Task<bool> UpdatePaymentStatusAsync(string transactionId, PaymentStatus newStatus)
        {
            var payment = await _paymentRepository.GetPaymentByTransactionIdAsync(transactionId);
            if (payment == null) return false;

            payment.PaymentStatus = newStatus;
            await _paymentRepository.UpdateAsync(payment);

            // If payment is successful, update the booking status
            if (newStatus == PaymentStatus.Completed)
            {
                var booking = await _bookingRepository.GetByIdAsync(payment.BookingId);
                if (booking != null)
                {
                    booking.BookingStatus = BookingStatus.Confirmed;
                    await _bookingRepository.UpdateAsync(booking);
                }
            }

            return true;
        }

        // How everything works
        // User books an event - Booking records as "Pending"
        // User initiate Payment - Payment is recorded as "Pending"
        //User complete payment on Flutterwave
        //Flutterwave  sends Callback - API update payment to "Completed"
        //API updates Booking to "Confirmed".
    }
}
