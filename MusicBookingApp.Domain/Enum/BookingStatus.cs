using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Domain.Enum
{
    public enum BookingStatus
    {
        Pending,     // Waiting for payment
        Confirmed,   // Payment completed, booking confirmed
        Cancelled    // Booking was cancelled
    }
}
