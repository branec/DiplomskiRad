using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Users.Factories;

namespace Users.Controllers
{
    public class MessageController : Controller
    {
		private MessageFactory messageFactory = null;

		public MessageController()
		{
			messageFactory = new MessageFactory();
		}
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult List(int? page)
		{
			page = (page ?? 1);
			return View(messageFactory.prepreMessages(User.Identity.Name,page.Value));
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = messageFactory.mRepository.Get(id.Value);
			if (message == null)
			{
				return HttpNotFound();
			}
			return View(message);
		}
    }
}