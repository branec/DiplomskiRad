using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Repositories;
using Data.Models;
using Users.Models;
using PagedList;

namespace Users.Factories
{
	public class TicketFactory
	{
		public TicketRepository tRepository = null;
		public TicketInventoryRepository tiRepository = null;
		public OrganisationRepository oRepository = null;
		public EventRepository eRepository = null;
		public TicketReservationRepository trRespository = null;

		public TicketFactory()
		{
			var entities = new Entities();
			tRepository = new TicketRepository(entities);
			tiRepository = new TicketInventoryRepository(entities);
			oRepository = new OrganisationRepository(entities);
			eRepository = new EventRepository(entities);
			trRespository = new TicketReservationRepository(entities);
		}

		public AddTicketModel prepreateAddTicketModel(int eventId)
		{
			AddTicketModel addTicketModel = new AddTicketModel();
			addTicketModel.EventId = eventId;

			List<Ticket> tickets = tRepository.getTicketsByEvent(eventId);
			foreach (var ticket in tickets)
			{
				TicketInventory ticketInventory = tiRepository.getTicketInventoryByTicket(ticket.Id);

				TicketViewModel ticketViewModel = new TicketViewModel()
				{
					Id = ticket.Id,
					Name = ticket.Name,
					Price = ticket.Price,
					DiscountPrice = ticket.DiscountPrice.HasValue?ticket.DiscountPrice.Value:0m,
					StockQuantity = ticketInventory.StockQuantity.HasValue?ticketInventory.StockQuantity.Value:0,
					ReservedQuantity = ticketInventory.ReservedQuantity.HasValue?ticketInventory.ReservedQuantity.Value:0
				};

				addTicketModel.Tickets.Add(ticketViewModel);
			}

			return addTicketModel;

		}

		public TicketInventory saveTicketWithInventory(Ticket ticket, TicketInventory ticketInventory)
		{
			tRepository.Add(ticket);
			tRepository.Complete();
			ticketInventory.IdTicket = ticket.Id;
			tiRepository.Add(ticketInventory);
			tiRepository.Complete();
			ticketInventory.Ticket = ticket;

			return ticketInventory;

		}

		public IPagedList<TicketListModel> prepareListOfTickets(string username, int page = 1)
		{
			Organisation organisation = oRepository.GetOrganisationByUsername(username);

			IPagedList<Event> events = eRepository.GetEventsByOrganisation(organisation.Id, page);

			List<TicketListModel> eventsList = new List<TicketListModel>();

			foreach (var e in events)
			{
				TicketListModel ticketListModel = new TicketListModel();
				ticketListModel.IdEvent = e.Id;
				ticketListModel.NameEvent = e.Name;

				List<Ticket> tickets = tRepository.getTicketsByEvent(e.Id);
				foreach (var ticket in tickets)
				{
					TicketInventory ticketInventory = tiRepository.getTicketInventoryByTicket(ticket.Id);

					TicketViewModel ticketViewModel = new TicketViewModel()
					{
						Id = ticket.Id,
						Name = ticket.Name,
						Price = ticket.Price,
						DiscountPrice = ticket.DiscountPrice.HasValue ? ticket.DiscountPrice.Value : 0m,
						StockQuantity = ticketInventory.StockQuantity.HasValue ? ticketInventory.StockQuantity.Value : 0,
						ReservedQuantity = ticketInventory.ReservedQuantity.HasValue ? ticketInventory.ReservedQuantity.Value : 0
					};

					ticketListModel.Tickets.Add(ticketViewModel);
				}
				eventsList.Add(ticketListModel);
			}

			IPagedList<TicketListModel> pageEvents = new StaticPagedList<TicketListModel>(eventsList, events.PageNumber, events.PageSize, events.TotalItemCount);

			return pageEvents;
		}

		public IPagedList<TicketReservation> prepareTicketReservations(string username, int page = 1)
		{
			Organisation organisation = oRepository.GetOrganisationByUsername(username);

			return trRespository.getTicketsReservationByOrganisation(organisation.Id, page);

		}


	}
}