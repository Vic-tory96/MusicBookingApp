using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Common;

namespace MusicBookingApp.Domain.Entities
{
    public class Booking : BaseEntity
    {
      
        public string EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string PaymentId { get; set; }  // Foreign key to Payment
        public Payment Payment { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    }
}
