using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Factories;

namespace Users.Controllers
{
    public class TicketController : Controller
    {
		private TicketFactory ticketFactory = null;

		public TicketController()
		{
			ticketFactory = new TicketFactory();
		}

        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult List( int? page)
		{
			page = (page ?? 1);

			return View(ticketFactory.prepareListOfTickets(User.Identity.Name,page.Value));
		}


		public ActionResult Reservation(int? page)
		{
			page = (page ?? 1);

			return View(ticketFactory.prepareTicketReservations(User.Identity.Name, page.Value));

		}
    }
}