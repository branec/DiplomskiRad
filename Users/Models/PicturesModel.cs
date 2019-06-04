using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
	public class PicturesModel
	{
		public PicturesModel()
		{
			Pictures = new List<PictureViewModel>();
		}

		public int IdEvent { get; set; }

		public int IdOrganisation { get; set; }

		public List<PictureViewModel> Pictures { get; set; }
	}
}