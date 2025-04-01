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
            var transactionRef = Guid.NewGuid().ToString();
            var paymentLink = await _flutterwaveService.InitializePaymentAsync(amount, email, transactionRef);

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentStatus = PaymentStatus.Pending,
                TransactionId = transactionRef,
                PaymentDate = DateTime.UtcNow
            };

            await _paymentRepository.CreatePaymentAsync(payment);
            return paymentLink;
        }

        //public async Task<Payment> ProcessPaymentAsync(string bookingId, decimal amount, PaymentMethod paymentMethod)
        //{
        //    var booking = await _bookingRepository.GetByIdAsync(bookingId);
        //    if (booking == null)
        //        throw new Exception("Booking not found.");

        //    var payment = new Payment
        //    {
        //        BookingId = bookingId,
        //        Amount = amount,
        //        PaymentMethod = paymentMethod,
        //        PaymentStatus = PaymentStatus.Pending,
        //        TransactionId = Guid.NewGuid().ToString(),
        //        PaymentDate = DateTime.UtcNow
        //    };

        //    return await _paymentRepository.CreatePaymentAsync(payment);
        //}

        public async Task<bool> UpdatePaymentStatusAsync(string transactionId, PaymentStatus newStatus)
        {
            var payment = await _paymentRepository.GetPaymentByTransactionIdAsync(transactionId);
            if (payment == null) return false;

            await _paymentRepository.UpdatePaymentStatusAsync(payment.Id, newStatus);
            return true;
        }
    }
}
