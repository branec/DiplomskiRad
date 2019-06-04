using Data.Interfaces;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(Entities eventsEntities) : base(eventsEntities)
        { }

        public IEnumerable<Event> GetEventsByPage(int pageIndex, int pageSize = 10)
        {
            return eventsEntities.Events
                    .Include(c => c.Location)
                    .OrderByDescending(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public IEnumerable<Event> GetFeaturedEvents(int count)
        {
            return eventsEntities.Events.OrderByDescending(e => e.Featured == true).Take(count).ToList();
        }

		public IPagedList<Event> GetEventsByOrganisation(int idOrganisation, int pageIndex = 1, int pageSize = 15)
		{
			return eventsEntities.Events
				   .Include(c => c.Location)
				   .Where(c=> c.IdOrganisation == idOrganisation)
				   .OrderByDescending(c => c.TimeStart)
				   .ToPagedList(pageIndex,pageSize);
		}

		public int GetEventsCountByOrganisation(int IdOrganisation)
		{
			return eventsEntities.Events
					.Where(c => c.IdOrganisation == IdOrganisation)
					.Count();
		}

		public Event GetMostInterestedEventByOrganisation(int idOrganisation)
		{
			return eventsEntities.Events
					.Where(c => c.IdOrganisation == idOrganisation)
					.OrderByDescending(c => c.Interesting)
					.FirstOrDefault();
		}

		public IEnumerable<Event> getMostInterestedEvents(int count)
		{
			return eventsEntities.Events
					.OrderByDescending(c => c.Interesting)
					.Take(count)
					.ToList();
		}

		public Entities eventsEntities
        {
            get { return Context as Entities; }
        }



    }
}