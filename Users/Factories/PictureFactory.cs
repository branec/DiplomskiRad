using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Models;
using Data.Repositories;
using Users.Models;

namespace Users.Factories
{
	public class PictureFactory
	{
		public PictureRepository pictureRepository = null;

		public PictureFactory()
		{
			pictureRepository = new PictureRepository(new Entities());
		}

		public int InsertPicture(byte[] binary, string name, int length, string extension, int idEvent = 0, int idOrganisation = 0)
		{

			Picture pic = new Picture()
			{
				FileName = name,
				Length = length,
				FileExtension = extension,
				FileBinary = binary,
				DateUploaded = DateTime.Now,
			};

			if (idEvent > 0)
				pic.IdEvent = idEvent;

			if (idOrganisation > 0)
				pic.IdOrganisation = idOrganisation;

			pictureRepository.Add(pic);
			pictureRepository.Complete();

			return pic.Id;
		}

		public List<PictureViewModel> getPictures(int IdEvent) {

			List<Picture> pictures = pictureRepository.getPicturesForEvent(IdEvent);
			List<PictureViewModel> picturesViewModel = new List<PictureViewModel>();
			foreach (var picture in pictures)
			{
				PictureViewModel pictureViewModel = new PictureViewModel()
				{
					Id = picture.Id,
					Name = picture.FileName,
					Exstension = picture.FileExtension
				};
				picturesViewModel.Add(pictureViewModel);

			}

			return picturesViewModel;

		}

		public List<PictureViewModel> getPicturesOrganisation(int IdOrganisation)
		{

			List<Picture> pictures = pictureRepository.getPicturesForOrganisation(IdOrganisation);
			List<PictureViewModel> picturesViewModel = new List<PictureViewModel>();
			foreach (var picture in pictures)
			{
				PictureViewModel pictureViewModel = new PictureViewModel()
				{
					Id = picture.Id,
					Name = picture.FileName,
					Exstension = picture.FileExtension
				};
				picturesViewModel.Add(pictureViewModel);

			}

			return picturesViewModel;

		}


	}
}