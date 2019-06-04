using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
	public class HomeModel
	{
		public decimal UserLatitude { get; set; }
		public decimal UserLongitude { get; set; }
		public string UserLocation { get; set; }

		public List<TypeEvent> EventTypes { get; set; }

		public List<Event> FeaturedEvents { get; set; }

		public List<Event> LatestEvents { get; set; }

		public List<Event> MostInterested { get; set; }
	}
}