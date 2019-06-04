using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Factories;
using Users.Models;

namespace Users.Controllers
{
    public class OrganisationController : Controller
    {
        // GET: Organisation
        OrganisationFactory oFactory = null;
		PictureFactory pFactory = null;

        public OrganisationController()
        {
            oFactory = new OrganisationFactory();
			pFactory = new PictureFactory();
        }
        public ActionResult Overview()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = oFactory.PrepareOrganisationOverViewModel(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Overview(OrganisationOverviewModel @organisationOverViewModel)
        {
            if (ModelState.IsValid)
            {
                oFactory.SaveOrganisationOverViewModel(organisationOverViewModel);
            }

            var model = oFactory.PrepareOrganisationOverViewModel(User.Identity.Name);
            return View(model);
        }

		public ActionResult PictureUpload()
		{
			Organisation organisation = oFactory.oRepository.GetOrganisationByUsername(User.Identity.Name);

			PicturesModel picturesModel = new PicturesModel();
			picturesModel.IdOrganisation = organisation.Id;
			picturesModel.Pictures = pFactory.getPicturesOrganisation(organisation.Id);

			return View(picturesModel);
		}
    }
}