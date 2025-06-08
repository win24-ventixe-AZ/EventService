

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string? Title { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime EventDate { get; set; } // includes the time of the event
    public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
    public string? Type { get; set; } = "Unknown"; 
    public string? Status { get; set; } = "Draft"; 
    public string? Location { get; set; } 
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; } // This is here to simplify the backend processes for now

    public ICollection<EventPackageEntity> Packages { get; set; } = [];
}
