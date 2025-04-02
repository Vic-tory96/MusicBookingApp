using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enum;

namespace MusicBookingApp.Application.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByBookingIdAsync(string bookingId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task UpdatePaymentStatusAsync(string paymentId, PaymentStatus newStatus);
    }
}
