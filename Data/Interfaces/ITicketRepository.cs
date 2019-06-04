﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface ITicketRepository : IRepository<Ticket>
	{
		List<Ticket> getTicketsByEvent(int idEvent);

		
	}
}
