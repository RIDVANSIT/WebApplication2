using WebApplication2.Core;

namespace WebApplication2.Entities;

public class CardType:Entity<Guid>
{
	public string Name { get; set; } //Mifare , Mifare 4k,RFID
}
