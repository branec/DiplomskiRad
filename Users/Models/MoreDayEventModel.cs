using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class MoreDayEventModel
	{
		public MoreDayEventModel()
		{
			EventDates = new List<EventDate>();
		}

		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }

		public int eventId { get; set; }

		public List<EventDate> EventDates { get; set; }
	}
}