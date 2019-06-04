using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class TicketListModel
	{
		public TicketListModel()
		{
			Tickets = new List<TicketViewModel>();
		}

		public int IdEvent { get; set; }

		public string NameEvent { get; set; }

		public List<TicketViewModel> Tickets { get; set; }


	}
}