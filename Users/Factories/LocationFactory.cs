using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Factories
{
    public class LocationFactory
    {
        public LocationRepository lRepository = null;

        public LocationFactory()
        {
            lRepository = new LocationRepository(new Entities());
        }
    }
}