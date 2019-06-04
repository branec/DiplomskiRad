using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using PagedList;

namespace Data.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetFeaturedEvents(int count);
        IEnumerable<Event> GetEventsByPage(int pageIndex, int pageSize);

		IPagedList<Event> GetEventsByOrganisation(int idOrganisation, int pageIndex = 1, int pageSize = 15);

		int GetEventsCountByOrganisation(int IdOrganisation);

		Event GetMostInterestedEventByOrganisation(int idOrganisation);

		IEnumerable<Event> getMostInterestedEvents(int count);


    }
}
