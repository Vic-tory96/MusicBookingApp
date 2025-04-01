using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enum;

namespace MusicBookingApp.Application.IServices
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(string bookingId, decimal amount, string email);
        Task<Payment?> GetPaymentByBookingIdAsync(string bookingId);
        Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task<bool> UpdatePaymentStatusAsync(string transactionId, PaymentStatus newStatus);
    }
}
