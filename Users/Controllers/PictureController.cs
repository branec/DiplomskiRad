using Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Factories;

namespace Users.Controllers
{
    public class PictureController : Controller
    {
		EventFactory eventFactory = null;
		PictureFactory pictureFactory = null;
		OrganisationFactory organisationFactory = null;
		public PictureController() {
			eventFactory = new EventFactory();
			pictureFactory = new PictureFactory();
			organisationFactory = new OrganisationFactory();
		}

        [HttpPost]
        public ActionResult Upload(int? eventId)
        {
            Stream stream = null;
            var fileName = "";
            var contentType = "";
            if (String.IsNullOrEmpty(Request["qqfile"]))
            {
                // IE
                HttpPostedFileBase httpPostedFile = Request.Files[0];
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                stream = httpPostedFile.InputStream;
                fileName = Path.GetFileName(httpPostedFile.FileName);
                contentType = httpPostedFile.ContentType;
            }
            else
            {
                //Webkit, Mozilla
                stream = Request.InputStream;
                fileName = Request["qqfile"];
            }

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

			Organisation organisation = organisationFactory.oRepository.GetOrganisationByUsername(User.Identity.Name);

			var pictureId = pictureFactory.InsertPicture(fileBinary, fileName, fileBinary.Length, fileExtension, eventId.HasValue?eventId.Value:0, eventId.HasValue?0:organisation.Id);

			//when returning JSON the mime-type must be set to text/plain
			//otherwise some browsers will pop-up a "Save As" dialog.
			if (eventId.HasValue)
			{
				return RedirectToAction("Create2", "Events", new { eventId = eventId.Value });
			}
			else {
				return RedirectToAction("PictureUpload", "Organisation");
			}
			
       
        }

		public ActionResult Delete(int pictureId, int? eventId)
		{
			Picture picture = pictureFactory.pictureRepository.Get(pictureId);
			pictureFactory.pictureRepository.Remove(picture);
			pictureFactory.pictureRepository.Complete();

			if (eventId.HasValue)
			{
				return RedirectToAction("Create2", "Events", new { eventId = eventId.Value });
			}
			else
			{
				return RedirectToAction("PictureUpload", "Organisation");
			}
			
		}

		public ActionResult Get(int pictureId) {

			Picture picture = pictureFactory.pictureRepository.Get(pictureId);
			byte[] bytes = picture.FileBinary;
			return File(bytes, "image/jpg");

		}


    }
}