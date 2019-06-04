using CreditCardValidator;
using Data.Models;
using Events.Factories;
using Events.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Events.Controllers
{
	public class EventController : Controller
	{
		private EventFactory eventFactory = null;

		public EventController() {
			eventFactory = new EventFactory();
		}

        [WebMethod]
        public ActionResult Interesting(int? eventId)
        {
			if (eventId.HasValue == false) {

				return Json(new { Success = "False"});
			}

			eventFactory.SetEventInterested(eventId.Value);
			return Json(new { Sucesss = "True"});
		}

		public ActionResult Details(int? IdEvent) {

			if (IdEvent.HasValue == false) {
				return HttpNotFound();
			}

			Event @event = eventFactory.prepareDeatilsModel(IdEvent.Value);

			return View(@event);
		}

		public ActionResult Reservation(int? IdEvent) {

			if (IdEvent.HasValue == false)
			{
				return HttpNotFound();
			}

			ViewBag.IdTicket = new SelectList(eventFactory.getTicketsForEvent(IdEvent.Value).ToList(), "Id", "Name");

			ReservationModelView reservationModelView = new ReservationModelView();
			reservationModelView.IdEvent = IdEvent.Value;

			return View(reservationModelView);

		}

		public ActionResult Message(int? IdEvent)
		{

			if (IdEvent.HasValue == false)
			{
				return HttpNotFound();
			}

			Message message = new Message();
			message.IdEvent = IdEvent.Value;

			return View(message);

		}

		[HttpPost]
		public ActionResult Message(Message message)
		{

			if (String.IsNullOrEmpty(message.Message1) || String.IsNullOrEmpty(message.FullName) )
			{
				return View(message);
			}

			eventFactory.AddNewMessage(message);

			return RedirectToAction("Details", "Event", new { IdEvent = message.IdEvent });

		}

		[HttpPost]
		public ActionResult Reservation(ReservationModelView rmv)
		{

			ViewBag.IdTicket = new SelectList(eventFactory.getTicketsForEvent(rmv.IdEvent).ToList(), "Id", "Name");

			if (String.IsNullOrEmpty(rmv.FullName) || String.IsNullOrEmpty(rmv.Email) || String.IsNullOrEmpty(rmv.number) || String.IsNullOrEmpty(rmv.FullName) || String.IsNullOrEmpty(rmv.expiry) || String.IsNullOrEmpty(rmv.cvc) || String.IsNullOrEmpty(rmv.name) || rmv.Quantity == 0)
			{
				rmv.ValidationMessage = "Sva polja moraju biti unesena.";
				return View(rmv);
			}

			CreditCardDetector detector = new CreditCardDetector(rmv.number);

			if (detector.IsValid() == false) {
				rmv.ValidationMessage = "Kartica nije valjana.";
				return View(rmv);
			}

			eventFactory.AddNewReservation(rmv);

			return RedirectToAction("Details", "Event", new { IdEvent = rmv.IdEvent });

		}

		[HttpPost]
		public ActionResult AddComment(ReviewEvent @eventReview) {

			eventFactory.AddComment(eventReview);

			return null;
		}

		public ActionResult List(string sortOrder, string currentFilter, string searchString, int? page) {

			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var events = eventFactory.eventRepository.GetAll();
			if (!String.IsNullOrEmpty(searchString))
			{
				//events = events.Where(s => s.Name.Contains(searchString)
				//					   || s.Organisation.Name.Contains(searchString));

				events = events.Where(s => s.Name.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					events = events.OrderByDescending(s => s.Name);
					break;
				case "Date":
					events = events.OrderBy(s => s.TimeStart);
					break;
				case "date_desc":
					events = events.OrderByDescending(s => s.TimeStart);
					break;
				default:  // Name ascending 
					events = events.OrderByDescending(s => s.Id);
					break;
			}

			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(events.ToPagedList(pageNumber, pageSize));
		}
    }
}