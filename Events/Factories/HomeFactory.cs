using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Events.Utils;
using Newtonsoft.Json;
using Data.Repositories;

namespace Events.Factories
{
	public class HomeFactory
	{
		public static string IpLocationAddress = "https://api.ipgeolocation.io/";
		public static string IpLocationPath = "ipgeo?apiKey=25ae91146bd7466f8285c647cf284dfd&ip=46.35.151.83";

		private EventRepository eventRepository = null;
		private TypeEventRepository typeEventRepository = null;

		public HomeFactory()
		{
			eventRepository = new EventRepository(new Data.Models.Entities());
			typeEventRepository = new TypeEventRepository(new Data.Models.Entities());
		}


		public HomeModel prepareHomeModel(string ipAddress) {

			HomeModel homeModel = new HomeModel();

			string jsonObject = ApiCall.Request(IpLocationAddress, IpLocationPath);
			UserLocationModel userLocationModel = JsonConvert.DeserializeObject<UserLocationModel>(jsonObject);

			homeModel.UserLocation = userLocationModel.city + ", " + userLocationModel.country_name;
			homeModel.UserLatitude = Decimal.Parse(userLocationModel.latitude);
			homeModel.UserLongitude = Decimal.Parse(userLocationModel.longitude);

			homeModel.FeaturedEvents = eventRepository.GetFeaturedEvents(10).ToList();
			homeModel.LatestEvents = eventRepository.GetEventsByPage(1, 10).ToList();
			homeModel.MostInterested = eventRepository.getMostInterestedEvents(10).ToList();

			homeModel.EventTypes = typeEventRepository.GetAll().ToList();


			return homeModel;

		}
	}
}