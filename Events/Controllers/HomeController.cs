using Data.Models;
using Data.Repositories;
using Events.Factories;
using Events.Models;
using Events.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
		private HomeFactory homeFactory = null;

		public HomeController() {
			homeFactory = new HomeFactory();
		}

        public ActionResult Index()
        {
			string ipAdress = Request.UserHostAddress.ToString();
			HomeModel homeModel = homeFactory.prepareHomeModel(ipAdress);

            return View(homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}