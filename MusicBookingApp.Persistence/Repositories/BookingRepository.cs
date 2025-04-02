
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Persistence.DBContext;

namespace MusicBookingApp.Persistence.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly MusicBookingDbContext _context;

        public BookingRepository(MusicBookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _context.Bookings
                  .Where(b => b.UserId == userId)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsForEventsByArtistIdAsync(string artistId)
        {
            return await _context.Bookings
                .Where(b => b.Event.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByUserAndEventAsync(string userId, string eventId)
        {
            return await _context.Bookings
            .FirstOrDefaultAsync(b => b.UserId == userId && b.EventId == eventId);
        }
    }
}
