using WebApplication2.Core;

namespace WebApplication2.Entities;

public class Order: Entity<Guid>
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public int Quantity {  get; set; } 
}
