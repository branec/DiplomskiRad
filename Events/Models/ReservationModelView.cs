using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
	public class ReservationModelView
	{
		public int IdEvent { get; set; }
		public int IdTicket { get; set; }
		public int Quantity { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }

		public string number { get; set; }
		public string expiry { get; set; }
		public string cvc { get; set; }
		public string name { get; set; }

		public string ValidationMessage { get; set; }


	}
}