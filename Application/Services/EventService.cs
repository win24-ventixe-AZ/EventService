using Application.Models;
using Azure.Core;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services;


/* 
 Technically, Price should not be a part of the EventEntity. I have added it because I am not sure if I have the time to do anything significant with PackageEntity in the frontend. Having it here at the moment adds one column, but reduces the complexity of the Services, and allows me to deliver a working Event Service quickly while still being scalable. 
 */
public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = request.Image, // Custom Image is not supported yet
                Title = request.Title,
                EventDate = request.EventDate,
                Type = request.Type ?? "Unknown",
                Status = request.EventDate == default ? "Draft" : (DateTime.UtcNow < request.EventDate ? "Active" : "Past"),
                Location = request.Location,
                Description = request.Description,
                Price = request.Price
            };

            var result = await _eventRepository.AddAsync(eventEntity);
            return result.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = result.Error };
        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<EventResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();
        var events = result.Result?.Select(x => new Event
        {
            Id = x.Id,
            Image = x.Image, // Custom Image is not supported yet
            Title = x.Title,
            EventDate = x.EventDate,
            Type = x.Type ?? "Unknown",
            Status = x.EventDate == default ? "Draft" : (DateTime.UtcNow < x.EventDate ? "Active" : "Past"),
            Location = x.Location,
            Description = x.Description,
            Price = x.Price
        });

        return new EventResult<IEnumerable<Event>> { Success = true, Result = events };
    }

    public async Task<EventResult<Event?>> GetEventAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);
        if (result.Success && result.Result != null)
        {
            var currentEvent = new Event
            {
                Id = result.Result.Id,
                Image = result.Result.Image, // Custom Image is not supported yet
                Title = result.Result.Title,
                EventDate = result.Result.EventDate,
                Type = result.Result.Type ?? "Unknown",
                Status = result.Result.EventDate == default ? "Draft" : (DateTime.UtcNow < result.Result.EventDate ? "Active" : "Past"),
                Location = result.Result.Location,
                Description = result.Result.Description,
                Price = result.Result.Price
            };

            return new EventResult<Event?> { Success = true, Result = currentEvent };
        }

        return new EventResult<Event?> { Success = false, Error = "Event not found." };
    }

    public async Task<EventResult> UpdateEventAsync(string eventId, CreateEventRequest request)
    {
        try
        {
            var existingResult = await _eventRepository.GetAsync(x => x.Id == eventId);
            if (!existingResult.Success || existingResult.Result is null)
            {
                return new EventResult
                {
                    Success = false,
                    Error = "Event not found."
                };
            }

            var existingEvent = existingResult.Result;

            existingEvent.Image = request.Image;
            existingEvent.Title = request.Title;
            existingEvent.EventDate = request.EventDate;
            existingEvent.Type = request.Type ?? "Unknown";
            existingEvent.Status = request.EventDate == default ? "Draft" : (DateTime.UtcNow < request.EventDate ? "Active" : "Past");
            existingEvent.Location = request.Location;
            existingEvent.Description = request.Description;
            existingEvent.Price = request.Price;

            var updateResult = await _eventRepository.UpdateAsync(existingEvent);
            return updateResult.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = updateResult.Error };
        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<EventResult> DeleteEventAsync(string eventId)
    {
        try
        {
            var existingResult = await _eventRepository.GetAsync(x => x.Id == eventId);
            if (!existingResult.Success || existingResult.Result is null)
            {
                return new EventResult
                {
                    Success = false,
                    Error = "Event not found."
                };
            }
            var deleteResult = await _eventRepository.DeleteAsync(existingResult.Result);
            return deleteResult.Success
                ? new EventResult { Success = true }
                : new EventResult { Success = false, Error = deleteResult.Error };
        }
        catch (Exception ex)
        {
            return new EventResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

}
