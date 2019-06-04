using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationByLatLng(decimal lat, decimal lng);

        Location GetLocationByAddressCity(string Address, string City);
    }
}
