using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class AddTicketModel
	{
		public AddTicketModel()
		{
			Tickets = new List<TicketViewModel>();
		}

		public int EventId { get; set; }
		public string Name { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }

		public decimal DiscountPrice { get; set; }

		public List<TicketViewModel> Tickets { get; set; }

	}
}