using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class TicketInventoryRepository: Repository<TicketInventory>, ITicketInventoryRepository
	{
		public TicketInventoryRepository(Entities Entities) : base(Entities)
		{
		}
		public Entities Entities
		{
			get { return Context as Entities; }
		}

		public decimal getPercetageOfReservedQuantity(int IdEvent)
		{
			int totalStock = Entities.TicketInventories
							 .Where(t => t.Ticket.IdEvent == IdEvent).ToList()
							 .Sum(t => t.StockQuantity.HasValue?t.StockQuantity.Value:0);

			int totalReserved = Entities.TicketInventories
							 .Where(t => t.Ticket.IdEvent == IdEvent).ToList()
							 .Sum(t => t.ReservedQuantity.HasValue ? t.ReservedQuantity.Value : 0);

			if (totalReserved == 0 || totalStock == 0)
				return 0;



			return ((decimal)totalReserved / (decimal) totalStock);
		}

		public TicketInventory getTicketInventoryByTicket(int idTicket)
		{
			return Entities.TicketInventories.SingleOrDefault(t => t.IdTicket == idTicket);
		}

	}
}
