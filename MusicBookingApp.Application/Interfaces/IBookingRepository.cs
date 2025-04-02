using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsForEventsByArtistIdAsync(string artistId);
        Task<ICollection<Booking>> GetBookingsByUserIdAsync(string userId);
        Task<Booking?> GetByUserAndEventAsync(string userId, string eventId);
    }
}
