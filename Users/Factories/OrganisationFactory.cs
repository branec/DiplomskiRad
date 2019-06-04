using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Models;
using Data.Models;
using System.Web.Mvc;

namespace Users.Factories
{
    public class OrganisationFactory
    {

        public OrganisationRepository oRepository = null;
        TypeOrganisationRepository toRepository = null;
        private LocationRepository lRepository = null;
		public EventRepository eRepository = null;
		public TicketInventoryRepository tiRepository = null;
		public MessageRepository mRepository = null;
		public TicketReservationRepository trRepository = null;


        public OrganisationFactory()
        {
			var entities = new Entities();
            oRepository = new OrganisationRepository(entities);
            toRepository = new TypeOrganisationRepository(entities);
            lRepository = new LocationRepository(entities);
			eRepository = new EventRepository(entities);
			tiRepository = new TicketInventoryRepository(entities);
			mRepository = new MessageRepository(entities);
			trRepository = new TicketReservationRepository(entities);
        }

        public int RegisterUser(RegistrationModel regModel)
        {
            Organisation org = new Organisation()
            {
                Username = regModel.Username,
                Password = regModel.Password,
                Email = regModel.Email,
                Name = regModel.OrganisationName,
                ActivationCode = regModel.ActivationCode,
                IdTypeOrganisation = regModel.OrganisationTypeId,
                IsActive = false
            };

            oRepository.Add(org);

            return oRepository.Complete();
        }

        public int ActivateUser(string activationCode)
        {
            Organisation org = oRepository.GetOrganisationByActivationCode(activationCode);

            if (org == null)
                return 0;

            org.IsActive = true;

           return oRepository.Complete();

        }

        public RegistrationModel PrepareRegistrationModel()
        {
            RegistrationModel model = new RegistrationModel();

            model.OrganisationTypes = prepareTypeOrganisationSelection();

            return model;

        }

        public OrganisationOverviewModel PrepareOrganisationOverViewModel(string userName) {

            OrganisationOverviewModel organisationOvewViewModel = null;
            Organisation organisation = oRepository.GetOrganisationByUsername(userName);

            if (organisation == null)
                return null;

            organisationOvewViewModel = new OrganisationOverviewModel() {
                Id = organisation.Id,
                Name = organisation.Name,
                IdTypeOrganisation = organisation.IdTypeOrganisation,
                Description = organisation.Description,
                Phone = organisation.Phone,
                Email = organisation.Email,
                Web = organisation.Web,
                PerDayWorkingHours = organisation.PerDayWorkingHours,
                WorkingHoursStart = organisation.WorkingHoursStart,
                WorkingHoursEnd = organisation.WorkingHoursEnd,
                WeekWorkingHoursStart = organisation.WeekWorkingHoursStart,
                WeekWorkingHoursEnd = organisation.WeekWorkingHoursEnd,
                SatWorkingHoursStart = organisation.SatWorkingHoursStart,
                SatWorkingHoursEnd = organisation.SatWorkingHoursEnd,
                SunWorkingHoursEnd = organisation.SunWorkingHoursEnd,
                SunWorkingHoursStart = organisation.SunWorkingHoursStart
            };

            organisationOvewViewModel.TypeOrganisationList = prepareTypeOrganisationSelection();

            if (organisation.IdLocation.HasValue)
            {
                organisationOvewViewModel.Latitude = organisation.Location.Lat.Value;
                organisationOvewViewModel.Longitude = organisation.Location.Lng.Value;
                organisationOvewViewModel.PostalCode = organisation.Location.PostalCode.Value.ToString();
                organisationOvewViewModel.Address = organisation.Location.Address;
                organisationOvewViewModel.City = organisation.Location.City;
                organisationOvewViewModel.Country = organisation.Location.Country;
            }

            return organisationOvewViewModel;
        }

        private List<SelectListItem> prepareTypeOrganisationSelection() {

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<TypeOrganisation> orgTypes = toRepository.GetAll();

            foreach (var orgType in orgTypes)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = orgType.Name;
                sli.Value = orgType.Id.ToString();
                selectListItems.Add(sli);
            }

            return selectListItems;

        }

        public void SaveOrganisationOverViewModel(OrganisationOverviewModel orgModel) {

            Location location = lRepository.GetLocationByAddressCity(orgModel.Address, orgModel.City);
            if (location == null && orgModel.Latitude > 0 && orgModel.Longitude > 0)
            {
                location = new Location()
                {
                    Lat = orgModel.Latitude,
                    Lng = orgModel.Longitude,
                    Address = orgModel.Address,
                    City = orgModel.City,
                    Country = orgModel.Country,
                    PostalCode = int.Parse(orgModel.PostalCode)
                };

                lRepository.Add(location);
                lRepository.Complete();
                location = lRepository.GetLocationByAddressCity(orgModel.Address, orgModel.City);

            }


            Organisation org = oRepository.Get(orgModel.Id);
            org.Name = orgModel.Name;
            org.IdTypeOrganisation = orgModel.IdTypeOrganisation;
            org.Description = orgModel.Description;
            org.Email = orgModel.Email;
            if (location != null)
                org.IdLocation = location.Id;

            org.Phone = orgModel.Phone;
            org.Web = orgModel.Web;

            //time
            org.PerDayWorkingHours = orgModel.PerDayWorkingHours;
            org.WorkingHoursStart = orgModel.WorkingHoursStart;
            org.WeekWorkingHoursStart = orgModel.WeekWorkingHoursStart;
            org.SunWorkingHoursStart = orgModel.SunWorkingHoursStart;
            org.SatWorkingHoursStart = orgModel.SatWorkingHoursStart;

            org.SatWorkingHoursEnd = orgModel.SatWorkingHoursEnd;
            org.SunWorkingHoursEnd = orgModel.SunWorkingHoursEnd;
            org.WeekWorkingHoursEnd = orgModel.WeekWorkingHoursEnd;
            org.WorkingHoursEnd = orgModel.WorkingHoursEnd;

            oRepository.Complete();

        }

		public DashboardModel prepareDashModel(string Username)
		{
			DashboardModel dashboardModel = new DashboardModel();
			Organisation organisation = oRepository.GetOrganisationByUsername(Username);

			dashboardModel.EventsCount = eRepository.GetEventsCountByOrganisation(organisation.Id);
			dashboardModel.MostInterestedEvent = eRepository.GetMostInterestedEventByOrganisation(organisation.Id);
			dashboardModel.MostInterestedEventPercetage = Decimal.Round(tiRepository.getPercetageOfReservedQuantity(dashboardModel.MostInterestedEvent.Id)*100);
			dashboardModel.UnreadedMessages = mRepository.getUnreadedMessagesByOrganisation(organisation.Id);
			dashboardModel.TodaysReservations = trRepository.getTodaysReservationsByOrganisation(organisation.Id);

			List<Event> lastEvents = eRepository.GetEventsByOrganisation(organisation.Id, pageSize: 5).ToList();

			foreach (Event @event in lastEvents)
			{
				DashEventModel dashEventModel = new DashEventModel() {
					Id = @event.Id,
					DateEnd = @event.TimeEnd.ToString("dd.MM.yyyy. hh:mm"),
					DateStart = @event.TimeStart.ToString("dd.MM.yyyy. hh:mm"),
					Name = @event.Name,
					NoTickets = @event.NoTickets,
					PercetnageReservation = Decimal.Round(tiRepository.getPercetageOfReservedQuantity(@event.Id)*100),
					ThirdPartyTickets = @event.ThirdPartyTickets
					};
				dashboardModel.LastEvents.Add(dashEventModel);
			}

			dashboardModel.LastReservations = trRepository.getTicketsReservationByOrganisation(organisation.Id, PageSize: 5).ToList();

			return dashboardModel;
		}



    }
}