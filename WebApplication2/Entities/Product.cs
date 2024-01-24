using System.ComponentModel.DataAnnotations;
using WebApplication2.Core;

namespace WebApplication2.Entities;

public class Product: Entity<Guid>
{
	public Guid CategoryId { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int StockAmount { get; set; }

    public string? Description { get; set; }
    public virtual Category Category { get; set; }
}