using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Factories;

namespace Users.Controllers
{
    public class UserController : Controller
    {
		private OrganisationFactory organisationFactory = null;

		public UserController()
		{
			organisationFactory = new OrganisationFactory();
		}


        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View(organisationFactory.prepareDashModel(User.Identity.Name));

        }
    }
}