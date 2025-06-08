
namespace Application.Models;

public class CreateEventRequest // This is a DTO
{
    public string? Image { get; set; } // There is no support for this in the frontend atm, but the backend logic has it in place.
    public string? Title { get; set; }
    public DateTime EventDate { get; set; }
    public string? Type { get; set; } = "Unknown"; // This is the Event type, can be "Social", "Concert", etc. I set default as Social for now.
    public string? Status { get; set; } = "Draft"; // Status = (TimeCreated - Date + Time) > TimeSpan.Zero ? "Active" : "Past"; 
    public string? Location { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; } // This is linked to Packages not Events and gotten from the frontend upon creation of an event.
}