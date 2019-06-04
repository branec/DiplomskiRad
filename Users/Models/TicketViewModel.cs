using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class TicketViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public decimal DiscountPrice { get; set; }

		public int StockQuantity { get; set; }

		public int ReservedQuantity { get; set; }


	}
}