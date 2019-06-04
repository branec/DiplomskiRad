using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(Entities Entities) : base(Entities)
        {
        }
        public Entities Entities
        {
            get { return Context as Entities; }
        }

        public Location GetLocationByAddressCity(string Address, string City)
        {
            return Entities.Locations.SingleOrDefault(o => o.Address.Trim().Equals(Address.Trim()) && o.City.Trim().Equals(City.Trim()));
        }

        public Location GetLocationByLatLng(decimal lat, decimal lng)
        {
            return Entities.Locations.SingleOrDefault(o => o.Lat.Value == lat && o.Lng.Value == lng);
        }
    }
}
