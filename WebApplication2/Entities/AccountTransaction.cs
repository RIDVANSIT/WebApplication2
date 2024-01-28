using WebApplication2.Core;

namespace WebApplication2.Entities;

public class AccountTransaction : Entity<Guid>
{
	public Guid CardId { get; set; }
	public decimal Balance { get; set; }
	public DateTime CreatedDate { get; set; }
	public virtual Card Card { get; set; }
}
