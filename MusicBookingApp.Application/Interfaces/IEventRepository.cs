using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsByArtistIdAsync(string artistId);
      
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<IEnumerable<Event>> GetPastEventsAsync();
        Task<Event?> GetEventWithDetailsAsync(string eventId);

    }
}
