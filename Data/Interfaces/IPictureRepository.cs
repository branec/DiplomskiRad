using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
		List<Picture> getPicturesForEvent(int IdEvent);
		List<Picture> getPicturesForOrganisation(int IdOrganisation);

	}
}
