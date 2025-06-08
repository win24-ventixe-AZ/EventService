

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities;

public class  PackageEntity  
{
    [Key]
    public int Id { get; set; }

    // The architercture supports significantly more complicated structures for the ticketing system. I have decided to not explore that for my MVP because of time. Most Events (in smaller to medium sized venues) tend to have a single type of ticket, so this is sufficient for now. 
    public string Title { get; set; } = "Standard Ticket"; 
    public string? SeatingArrangement { get; set; }
    public string? Placement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public Decimal? Price { get; set; }
    public string? Currency { get; set; }

}
