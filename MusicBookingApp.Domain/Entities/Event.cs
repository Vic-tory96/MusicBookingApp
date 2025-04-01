using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Common;

namespace MusicBookingApp.Domain.Entities
{
    public class Event : BaseEntity
    {
       
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
