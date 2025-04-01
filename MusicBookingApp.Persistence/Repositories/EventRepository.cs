using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Persistence.DBContext;

namespace MusicBookingApp.Persistence.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly MusicBookingDbContext _context;

        public EventRepository(MusicBookingDbContext context) : base(context)
        {
            _context = context;
        }

        // Get all events where a specific artist is performing
        public async Task<IEnumerable<Event>> GetEventsByArtistIdAsync(string artistId)
        {
            return await _context.Events
                .Where(e => e.ArtistId == artistId)
                .ToListAsync();
        }

        // Get upcoming events
        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Date >= DateTime.UtcNow)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        // Get past events
        public async Task<IEnumerable<Event>> GetPastEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Date < DateTime.UtcNow)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        // Get an event with full details (including Artist and associated Bookings)
        public async Task<Event?> GetEventWithDetailsAsync(string eventId)
        {
            return await _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Bookings)
                    .ThenInclude(b => b.User) // Include User details in the bookings
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }


    }
}
