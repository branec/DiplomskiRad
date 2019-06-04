using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class DashEventModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DateStart { get; set; }
		public string DateEnd { get; set; }
		public bool NoTickets { get; set; }
		public bool ThirdPartyTickets { get; set; }
		public decimal PercetnageReservation { get; set; }
	}
}