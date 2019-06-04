using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Users.Factories;
using PagedList;
using Users.Models;

namespace Users.Controllers
{
    public class EventsController : Controller
    {
        private EventFactory eFactory = null;
		private TicketFactory tFactory = null;
		private PictureFactory pFactory = null;

        public EventsController()
        {
            eFactory = new EventFactory();
			tFactory = new TicketFactory();
			pFactory = new PictureFactory();
        }

        // GET: Events
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

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


           
			Organisation organisation = eFactory.oRepository.GetOrganisationByUsername(User.Identity.Name);
            int pageNumber = (page ?? 1);
            return View(eFactory.eRepository.GetEventsByOrganisation(organisation.Id,pageNumber)); 
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = eFactory.eRepository.Get(id.Value);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.IdTypeEvent = new SelectList(eFactory.teRepository.GetAll(), "Id", "Name");
            return View(new EventModel());
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventModel @event)
        {
            if (ModelState.IsValid)
            {

                Event eventDB = eFactory.InsertEventModel(@event, User.Identity.Name);

                return RedirectToAction("Create2",eventDB);
            }

            ViewBag.IdTypeEvent = new SelectList(eFactory.teRepository.GetAll(), "Id", "Name", @event.IdTypeEvent);
            return View(@event);
        }


        public ActionResult Create2(int eventId)
        {

			PicturesModel picturesModel = new PicturesModel();
			picturesModel.IdEvent = eventId;
			picturesModel.Pictures = pFactory.getPictures(eventId);
	
            return View(picturesModel);
        }



		public ActionResult Create3(int eventId)
        {
			MoreDayEventModel mdeModel = new MoreDayEventModel();
			mdeModel.eventId = eventId;
			mdeModel.EventDates = eFactory.getEventDates(eventId).ToList();

            return View(mdeModel);
        }

		[HttpPost]
		public ActionResult Create3(MoreDayEventModel moreDayEvent)
		{

			EventDate eventDate = new EventDate()
			{
				DateStart = moreDayEvent.DateStart,
				DateEnd = moreDayEvent.DateEnd,
				IdEvent = moreDayEvent.eventId
			};

			eventDate = eFactory.insertEventDate(eventDate);

			moreDayEvent.EventDates = eFactory.getEventDates(moreDayEvent.eventId).ToList();

			moreDayEvent.DateStart = new DateTime();
			moreDayEvent.DateEnd = new DateTime();

			return View(moreDayEvent);
		}

		public ActionResult Create4(int eventId)
		{
			return View(tFactory.prepreateAddTicketModel(eventId));
		}

		[HttpPost]
		public ActionResult Create4(AddTicketModel addTicketModel)
		{

			Ticket ticket = new Ticket()
			{
				Name = addTicketModel.Name,
				Price = addTicketModel.Price,
				DiscountPrice = addTicketModel.DiscountPrice,
				IdEvent = addTicketModel.EventId
			};


			TicketInventory ticketInventory = new TicketInventory()
			{
				ReservedQuantity = 0,
				StockQuantity = addTicketModel.Quantity
			};

			tFactory.saveTicketWithInventory(ticket, ticketInventory);

			return View(tFactory.prepreateAddTicketModel(addTicketModel.EventId));
		}

		public ActionResult DeleteEventDate(int eventDateId,int eventId)
		{
			EventDate eventDate = eFactory.edRepository.Get(eventDateId);
			eFactory.edRepository.Remove(eventDate);
			eFactory.edRepository.Complete();

			return RedirectToAction("Create3", new { eventId = eventId });
		}

		public ActionResult DeleteTicket(int ticketId, int eventId)
		{
			Ticket ticket = tFactory.tRepository.Get(ticketId);
			TicketInventory ticketInventory = tFactory.tiRepository.getTicketInventoryByTicket(ticketId);

			tFactory.tiRepository.Remove(ticketInventory);
			tFactory.tiRepository.Complete();
			tFactory.tRepository.Remove(ticket);
			tFactory.tRepository.Complete();

			return RedirectToAction("Create4", new { eventId = eventId });
		}



		// GET: Events/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			EventModel eventModel = eFactory.PrepareEditModel(id.Value);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTypeEvent = new SelectList(eFactory.teRepository.GetAll(), "Id", "Name", eventModel.IdTypeEvent);
            return View(eventModel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventModel @event)
        {
            if (ModelState.IsValid)
            {
				eFactory.ModifyEventModel(@event);
                return RedirectToAction("Index");
            }


            ViewBag.IdTypeEvent = new SelectList(eFactory.teRepository.GetAll(), "Id", "Name", @event.IdTypeEvent);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = eFactory.eRepository.Get(id.Value);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = eFactory.eRepository.Get(id);
            eFactory.eRepository.Remove(@event);
            eFactory.eRepository.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
            }
            base.Dispose(disposing);
        }
    }
}
