using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Models;
using Events.Models;
using PagedList;

namespace Events.Factories
{
	public class EventFactory
	{
		public EventRepository eventRepository = null;
		private ReviewEventRepository reviewEventRepository = null;
		private TicketRepository ticketRepository = null;
		private TicketReservationRepository ticketReservationRepository = null;
		private CreditCardRepository creditCardRepository = null;
		private MessageRepository messageRepository = null;

		public EventFactory() {
			Entities entities = new Entities();
			eventRepository = new EventRepository(entities);
			reviewEventRepository = new ReviewEventRepository(entities);
			ticketRepository = new TicketRepository(entities);
			ticketReservationRepository = new TicketReservationRepository(entities);
			creditCardRepository = new CreditCardRepository(entities);
			messageRepository = new MessageRepository(entities);
		}

		public void SetEventInterested(int IdEvent) {

			Event @event = eventRepository.Get(IdEvent);
			@event.Interesting = @event.Interesting + 1;
			eventRepository.Complete();

		}

		public Event prepareDeatilsModel(int IdEvent) {

			return eventRepository.Get(IdEvent);

		}

		public void AddComment(ReviewEvent reviewEvent) {

			reviewEventRepository.Add(reviewEvent);
			reviewEventRepository.Complete();

		}

		public IEnumerable<Ticket> getTicketsForEvent(int IdEvent)
		{
			return ticketRepository.getTicketsByEvent(IdEvent);

		}

		public void AddNewReservation(ReservationModelView reservationModelView) {

			string[] dates = reservationModelView.expiry.Split('/');
			int mount = int.Parse(dates[0]);
			int year = int.Parse(dates[1]) + 2000;

			CreditCard creditCard = new CreditCard() {
				NameSurname = reservationModelView.name,
				CardNumber = reservationModelView.number,
				ExpDate = new DateTime(year,mount,1),
				CVV = reservationModelView.cvc
			};

			creditCardRepository.Add(creditCard);
			creditCardRepository.Complete();

			TicketReservation ticketReservation = new TicketReservation()
			{
				IdCreditCard = creditCard.Id,
				IdTicket = reservationModelView.IdTicket,
				Created = DateTime.Now,
				Email = reservationModelView.Email,
				FullName = reservationModelView.FullName,
				Quantity = reservationModelView.Quantity
			};

			ticketReservationRepository.Add(ticketReservation);
			ticketReservationRepository.Complete();

		}

		public void AddNewMessage(Message message) {
			message.Created = DateTime.Now;
			message.Readed = false;
			messageRepository.Add(message);
			messageRepository.Complete();
		}


	}
}