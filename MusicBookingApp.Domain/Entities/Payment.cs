using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Common;
using MusicBookingApp.Domain.Enum;

namespace MusicBookingApp.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string BookingId { get; set; }  // Foreign key to Booking
        public Booking Booking { get; set; }  // Navigation property to Booking

        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; }
        public string TransactionId { get; set; }  // For storing payment transaction ID from a provider (e.g., Stripe)
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    }
}
