using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId);
        Task<Booking?> CreateBookingAsync(string userId, string eventId);
        Task<Booking?> UpdateBookingAsync(string bookingId, Booking updatedBooking);
        Task<bool> DeleteBookingAsync(string bookingId);
    }
}
