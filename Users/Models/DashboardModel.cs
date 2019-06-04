using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class DashboardModel
	{
		public DashboardModel()
		{
			LastEvents = new List<DashEventModel>();
			LastReservations = new List<TicketReservation>();
		}

		public int EventsCount { get; set; }
		public int UnreadedMessages { get; set; }
		public int TodaysReservations { get; set; }
		public Event MostInterestedEvent { get; set; }

		public decimal MostInterestedEventPercetage { get; set; }

		public List<DashEventModel> LastEvents { get; set; }

		public List<TicketReservation> LastReservations { get; set; }



	}
}