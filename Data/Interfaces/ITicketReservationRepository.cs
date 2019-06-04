using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface ITicketReservationRepository: IRepository<TicketReservation>
	{
		IPagedList<TicketReservation> getTicketsReservationByOrganisation(int idOrganisation, int pageIndex = 1, int PageSize = 15);

		int getTodaysReservationsByOrganisation(int IdOrganisation);

		
	}
}
