using Data.Interfaces;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public MessageRepository(Entities Entities) : base(Entities)
		{
		}
		public Entities Entities
		{
			get { return Context as Entities; }
		}

		public IPagedList<Message> getMessagesByOrganisation(int IdOrganisation, int pageIndex = 1, int pageSize = 15)
		{
			return Entities.Messages
					.Where(m => m.Event.IdOrganisation == IdOrganisation)
					.OrderByDescending(m => m.Id)
					.ToPagedList(pageIndex, 2);
		}

		public int getUnreadedMessagesByOrganisation(int IdOrganisation)
		{
			return Entities.Messages
					.Where(m => m.Event.IdOrganisation == IdOrganisation && m.Readed.Value == false)
					.Count();
		}
	}
}
