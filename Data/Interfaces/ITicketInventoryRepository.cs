using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface ITicketInventoryRepository : IRepository<TicketInventory>
	{
		TicketInventory getTicketInventoryByTicket(int idTicket);

		decimal getPercetageOfReservedQuantity(int IdEvent);

	}
}
