using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IMessageRepository : IRepository<Message>
	{
		IPagedList<Message> getMessagesByOrganisation(int IdOrganisation, int pageIndex = 1, int pageSize = 15);

		int getUnreadedMessagesByOrganisation(int IdOrganisation);
	}
}
