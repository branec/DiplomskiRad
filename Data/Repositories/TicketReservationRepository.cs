using Data.Interfaces;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class TicketReservationRepository : Repository<TicketReservation>, ITicketReservationRepository
	{
		public TicketReservationRepository(Entities Entities) : base(Entities)
		{
		}
		public Entities Entities
		{
			get { return Context as Entities; }
		}


		public IPagedList<TicketReservation> getTicketsReservationByOrganisation(int idOrganisation, int pageIndex = 1, int PageSize = 15)
		{
			return Entities.TicketReservations
				   .Where(c => c.Ticket.Event.IdOrganisation == idOrganisation)
				   .OrderByDescending(c => c.Id)
				   .ToPagedList(pageIndex, 2);
		}

		public int getTodaysReservationsByOrganisation(int IdOrganisation)
		{
			DateTime today = DateTime.Now.Date;
			return Entities.TicketReservations
					.Where(t => t.Ticket.Event.IdOrganisation == IdOrganisation && (t.Created.Value.Year == today.Year && t.Created.Value.Month == today.Month && t.Created.Value.Day == today.Day))
					.Count();
		}
	}
}
