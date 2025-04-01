using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.IServices
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(string eventId);
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event?> UpdateEventAsync(string eventId, Event updatedEvent);
        Task<bool> DeleteEventAsync(string eventId);
    }
}
