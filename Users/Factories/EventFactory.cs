using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Repositories;
using Data.Models;
using Users.Models;

namespace Users.Factories
{
    public class EventFactory
    {
        public EventRepository eRepository = null;
        public LocationRepository lRepository = null;
        public OrganisationRepository oRepository = null;
        public TypeEventRepository teRepository = null;
		public PictureRepository pRepository = null;
		public EventDateRepository edRepository = null;
        public EventFactory()
        {
            eRepository = new EventRepository(new Entities());
            lRepository = new LocationRepository(new Entities());
            oRepository = new OrganisationRepository(new Entities());
            teRepository = new TypeEventRepository(new Entities());
			pRepository = new PictureRepository(new Entities());
			edRepository = new EventDateRepository(new Entities());
        }

        public Event InsertEventModel(EventModel eventModel, string username) {

            Organisation organisation = oRepository.GetOrganisationByUsername(username);

            Location location = lRepository.GetLocationByAddressCity(eventModel.Address, eventModel.City);
            if (location == null && eventModel.Latitude > 0 && eventModel.Longitude > 0)
            {
                location = new Location()
                {
                    Lat = eventModel.Latitude,
                    Lng = eventModel.Longitude,
                    Address = eventModel.Address,
                    City = eventModel.City,
                    Country = eventModel.Country,
                    PostalCode = int.Parse(eventModel.PostalCode)
                };

                lRepository.Add(location);
                lRepository.Complete();
                location = lRepository.GetLocationByAddressCity(eventModel.Address, eventModel.City);

            }

            Event eventDB = new Event() {
                Name = eventModel.Name,
                Description = eventModel.Description,
                IdTypeEvent = eventModel.IdTypeEvent,
                IdOrganisation = organisation.Id,
                NoTickets = eventModel.NoTickets,
                MoreDayEvent = eventModel.MoreDayEvent,
                TimeEnd = DateTime.Parse(eventModel.TimeEnd),
                TimeStart = DateTime.Parse(eventModel.TimeStart),
                Wifi = eventModel.Wifi,
                Parking = eventModel.Parking,
                Smoking = eventModel.Smoking,
                WebUrl = eventModel.WebUrl,
                Phone = eventModel.Phone,
                Email = eventModel.Email,
                ThirdPartyTickets = eventModel.ThirdPartyTickets,
                ThirdPartyTicketsWeb = eventModel.ThirdPartyTicketsWeb
             };

            if (location != null)
                eventDB.IdLocation = location.Id;

            eRepository.Add(eventDB);
            eRepository.Complete();

            return eventDB;

        }

		public Event ModifyEventModel(EventModel eventModel)
		{

			Event e = eRepository.Get(eventModel.Id);
			Location location = lRepository.GetLocationByAddressCity(eventModel.Address, eventModel.City);

			e.Name = eventModel.Name;
			e.Description = eventModel.Description;
			e.IdTypeEvent = eventModel.IdTypeEvent;
			e.NoTickets = eventModel.NoTickets;

			if (location == null && eventModel.Latitude > 0 && eventModel.Longitude > 0)
			{
				location = new Location()
				{
					Lat = eventModel.Latitude,
					Lng = eventModel.Longitude,
					Address = eventModel.Address,
					City = eventModel.City,
					Country = eventModel.Country,
					PostalCode = int.Parse(eventModel.PostalCode)
				};

				lRepository.Add(location);
				lRepository.Complete();
				location = lRepository.GetLocationByAddressCity(eventModel.Address, eventModel.City);

			}

			e.IdLocation = location.Id;
			e.MoreDayEvent = eventModel.MoreDayEvent;
			e.TimeEnd = DateTime.Parse(eventModel.TimeEnd);
			e.TimeStart = DateTime.Parse(eventModel.TimeStart);
			e.Wifi = eventModel.Wifi;
			e.Parking = eventModel.Parking;
			e.Smoking = eventModel.Smoking;
			e.WebUrl = eventModel.WebUrl;
			e.Email = eventModel.Email;
			e.Phone = eventModel.Phone;
			e.ThirdPartyTickets = eventModel.ThirdPartyTickets;
			e.ThirdPartyTicketsWeb = eventModel.ThirdPartyTicketsWeb;

			eRepository.Complete();
			return e;

		}

		public EventModel PrepareEditModel(int idEvent)
		{
			Event e = eRepository.Get(idEvent);

			EventModel eventModel = new EventModel()
			{
				Id = e.Id,
				Address = e.Location.Address,
				City = e.Location.City,
				Country =e.Location.Country,
				Description = e.Description,
				Email = e.Email,
				IdTypeEvent = e.IdTypeEvent.HasValue ? e.IdTypeEvent.Value : 0,
				Latitude = e.Location.Lat.HasValue ? e.Location.Lat.Value : 0m,
				Longitude = e.Location.Lng.HasValue ? e.Location.Lng.Value :0m,
				MoreDayEvent = e.MoreDayEvent,
				Name = e.Name,
				NoTickets = e.NoTickets,
				Parking = e.Parking.HasValue ? e.Parking.Value : false,
				Phone = e.Phone,
				PostalCode = e.Location.PostalCode.HasValue ? e.Location.PostalCode.Value.ToString() : string.Empty,
				Smoking = e.Smoking.HasValue ? e.Smoking.Value : false,
				ThirdPartyTickets = e.ThirdPartyTickets,
				ThirdPartyTicketsWeb = e.ThirdPartyTicketsWeb,
				TimeEnd = e.TimeEnd.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T'),
				TimeStart = e.TimeStart.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T'),
				WebUrl = e.WebUrl,
				Wifi = e.Wifi.HasValue ? e.Wifi.Value : false
			};

			return eventModel;
		}

	
		public EventDate insertEventDate(EventDate eventDate)
		{
			edRepository.Add(eventDate);
			int rowsAffected = edRepository.Complete();

			if (rowsAffected == 1)
			{
				return eventDate;
			}

			return null;
		}

		public IList<EventDate> getEventDates(int eventId)
		{
			return edRepository.getEventDates(eventId);
		}




    }
}