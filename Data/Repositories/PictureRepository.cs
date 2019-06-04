using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        public PictureRepository(Entities Entities) : base(Entities)
        {
        }
        public Entities Entities
        {
            get { return Context as Entities; }
        }

		public List<Picture> getPicturesForEvent(int IdEvent)
		{
			return Entities.Pictures.Where(p => p.IdEvent == IdEvent).ToList();
		}

		public List<Picture> getPicturesForOrganisation(int IdOrganisation)
		{
			return Entities.Pictures.Where(p => p.IdOrganisation == IdOrganisation).ToList();
		}
	}
}
