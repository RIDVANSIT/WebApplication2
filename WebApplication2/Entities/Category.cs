using System.ComponentModel.DataAnnotations;
using WebApplication2.Core;

namespace WebApplication2.Entities;

public class Category : Entity<Guid>
{
	[MaxLength(50)]
	public required string Name { get; set; }

	public virtual ICollection<Product> Products { get; set; }
	public Category()
	{
		Products = new HashSet<Product>();
	}
}
