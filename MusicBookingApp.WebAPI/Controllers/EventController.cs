using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Response;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(new ApiResponse<IEnumerable<EventDto>>(true, 200, "Events retrieved successfully.", eventDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null)
                return NotFound(new ApiResponse<EventDto>(false, 404, "Event not found.", null));

            var eventDto = _mapper.Map<EventDto>(eventEntity);
            return Ok(new ApiResponse<EventDto>(true, 200, "Event retrieved successfully.", eventDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            var createdEvent = await _eventService.CreateEventAsync(eventEntity);
            var createdEventDto = _mapper.Map<EventDto>(createdEvent);
            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, new ApiResponse<EventDto>(true, 201, "Event created successfully.", createdEventDto));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, [FromBody] EventDto eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            var updatedEvent = await _eventService.UpdateEventAsync(id, eventEntity);
            if (updatedEvent == null)
                return NotFound(new ApiResponse<EventDto>(false, 404, "Event not found.", null));

            var updatedEventDto = _mapper.Map<EventDto>(updatedEvent);
            return Ok(new ApiResponse<EventDto>(true, 200, "Event updated successfully.", updatedEventDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<bool>(false, 404, "Event not found.", false));

            return Ok(new ApiResponse<bool>(true, 200, "Event deleted successfully.", true));
        }
    }
}
