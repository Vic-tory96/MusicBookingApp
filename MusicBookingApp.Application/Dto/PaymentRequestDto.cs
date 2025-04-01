using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Application.Dto
{
    public class PaymentRequestDto
    {
        public string BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Email { get; set; }
    }
}
