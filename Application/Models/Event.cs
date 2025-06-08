
namespace Application.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string? Title { get; set; }

    public DateTime EventDate { get; set; } // includes the time of the event
    public DateTime TimeCreated { get; set; } 
    public string? Type { get; set; } = "Unknown";
    public string? Status { get; set; } = "Draft";
    public string? Location { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; } // This is here to simplify the backend processes for now
}
