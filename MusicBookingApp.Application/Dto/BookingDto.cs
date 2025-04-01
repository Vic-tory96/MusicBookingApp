using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Application.Dto
{
    public class BookingDto
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
