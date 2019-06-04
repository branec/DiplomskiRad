using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Users.Models
{
    public class EventModel
    {
		public int Id { get; set; }
		//General
		[DisplayName("Naziv")]
		public string Name { get; set; }

        [DataType(DataType.MultilineText)]
		[DisplayName("Opis")]
		public string Description { get; set; }

		[DisplayName("Tip dogadaja")]
		public int IdTypeEvent { get; set; }

		[DisplayName("Bez karata")]
		public bool NoTickets { get; set; }

        //Location
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

		[DisplayName("Adresa")]
		public string Address { get; set; }

		[DisplayName("Grad")]
		public string City { get; set; }

		[DisplayName("Drzava")]
		public string Country { get; set; }

		[DisplayName("Postanski broj")]
		public string PostalCode { get; set; }

		//Time
		[DisplayName("Dogadaj se odrzava vise dana")]
		public bool MoreDayEvent { get; set; }

		[DisplayName("Vrijeme pocetka")]
		public string TimeStart { get; set; }

		[DisplayName("Vrijeme kraja")]
		public string TimeEnd { get; set; }

		//Services
		[DisplayName("Internet")]
		public bool Wifi { get; set; }

		[DisplayName("Parking")]
		public bool Parking { get; set; }

		[DisplayName("Pusenje")]
		public bool Smoking { get; set; }

		//Additional

		[DisplayName("Web mjesto")]
		public string WebUrl { get; set; }

		[DisplayName("Email")]
		public string Email { get; set; }

		[DisplayName("Telefon")]
		public string Phone { get; set; }

		[DisplayName("Koristimo dispatchera karata")]
		public bool ThirdPartyTickets { get; set; }

		[DisplayName("Web mjesto za karte")]
		public string ThirdPartyTicketsWeb { get; set; }




    }
}