using Data.Models;
using Data.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Factories
{
	public class MessageFactory
	{
		public MessageRepository mRepository = null;
		public OrganisationRepository oRepository = null;

		public MessageFactory()
		{
			var entities = new Entities();
			mRepository = new MessageRepository(entities);
			oRepository = new OrganisationRepository(entities);

		}

		public IPagedList<Message> prepreMessages(string username, int page = 1)
		{
			Organisation organisation = oRepository.GetOrganisationByUsername(username);

			return mRepository.getMessagesByOrganisation(organisation.Id,page);
		}
	}
}