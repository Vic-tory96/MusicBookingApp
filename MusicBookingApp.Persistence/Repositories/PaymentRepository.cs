using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enum;
using MusicBookingApp.Persistence.DBContext;

namespace MusicBookingApp.Persistence.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly MusicBookingDbContext _context;

        public PaymentRepository(MusicBookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }


        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Booking)
                .ToListAsync();
        }
        public async Task<Payment?> GetPaymentByBookingIdAsync(string bookingId)
        {
            return await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task<Payment?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId)
        {
            return await _context.Payments
                .Where(p => p.Booking.UserId == userId)
                .Include(p => p.Booking)
                .ToListAsync();
        }

        public async Task UpdatePaymentStatusAsync(string paymentId, PaymentStatus newStatus)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                payment.PaymentStatus = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
