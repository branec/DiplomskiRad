using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class EventDateRepository : Repository<EventDate>, IEventDateRepository
	{
		public EventDateRepository(Entities Entities) : base(Entities)
		{
		}
		public Entities Entities
		{
			get { return Context as Entities; }
		}

		public IList<EventDate> getEventDates(int eventId)
		{
			return Entities.EventDates.Where(e => e.IdEvent == eventId).OrderBy(e => e.DateStart).ToList();
		}
	}
}
