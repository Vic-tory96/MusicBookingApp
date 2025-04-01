using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Persistence.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event?> GetEventByIdAsync(string eventId)
        {
            return await _eventRepository.GetByIdAsync(eventId);
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            return await _eventRepository.AddAsync(newEvent);
        }

        public async Task<Event?> UpdateEventAsync(string eventId, Event updatedEvent)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(eventId);
            if (existingEvent == null) return null;

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Date = updatedEvent.Date;
            existingEvent.Location = updatedEvent.Location;
            // Add other fields to update

            await _eventRepository.UpdateAsync(existingEvent);
            return existingEvent;
        }

        public async Task<bool> DeleteEventAsync(string eventId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null) return false;

            await _eventRepository.DeleteAsync(eventEntity);
            return true;
        }
    }
}
