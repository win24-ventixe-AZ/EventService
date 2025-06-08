using Application.Models;

namespace Application.Services
{
    public interface IEventService
    {
        Task<EventResult> CreateEventAsync(CreateEventRequest request);
        Task<EventResult> DeleteEventAsync(string eventId);
        Task<EventResult<Event?>> GetEventAsync(string eventId);
        Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
        Task<EventResult> UpdateEventAsync(string eventId, CreateEventRequest request);
    }
}