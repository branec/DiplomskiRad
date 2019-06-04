using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class TicketRepository : Repository<Ticket>, ITicketRepository
	{
		public TicketRepository(Entities Entities) : base(Entities)
		{
		}
		public Entities Entities
		{
			get { return Context as Entities; }
		}


		public List<Ticket> getTicketsByEvent(int idEvent)
		{
			return Entities.Tickets.Where(t => t.IdEvent == idEvent).ToList();
		}
	}
}
