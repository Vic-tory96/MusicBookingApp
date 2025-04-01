using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBookingApp.Application.Dto
{
    public class EventDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string ArtistId { get; set; }
    }
}
