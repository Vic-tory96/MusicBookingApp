using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Persistence.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
       
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
           
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _bookingRepository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<Booking?> CreateBookingAsync(string userId, string eventId)
        {
            //check if the user already booked this event
            var existingBooking = await _bookingRepository.GetByUserAndEventAsync(userId, eventId);
            if (existingBooking != null) return null; //Prevent duplicate booking of a particular event

            var booking = new Booking
            {
               UserId = userId,
               EventId = eventId,
               BookingDate = DateTime.UtcNow
            };

            return await _bookingRepository.AddAsync(booking);
        }

        public async Task<Booking?> UpdateBookingAsync(string bookingId, Booking updatedBooking)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(bookingId);
            if (existingBooking == null) return null;

            existingBooking.EventId = updatedBooking.EventId;
            existingBooking.UserId = updatedBooking.UserId;
            existingBooking.BookingDate = updatedBooking.BookingDate;

            await _bookingRepository.UpdateAsync(existingBooking);
            return existingBooking;
        }

        public async Task<bool> DeleteBookingAsync(string bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            await _bookingRepository.DeleteAsync(booking);
            return true;
        }
    }
}
