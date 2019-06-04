using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Users.Models
{
    public class OrganisationOverviewModel
    {
        public OrganisationOverviewModel()
        {
            TypeOrganisationList = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public int IdTypeOrganisation { get; set; }
        public IList<SelectListItem> TypeOrganisationList { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Name { get; set; }
        public bool PerDayWorkingHours { get; set; }
        public Nullable<System.DateTime> WorkingHoursStart { get; set; }
        public Nullable<System.DateTime> WorkingHoursEnd { get; set; }
        public Nullable<System.DateTime> WeekWorkingHoursStart { get; set; }
        public Nullable<System.DateTime> WeekWorkingHoursEnd { get; set; }
        public Nullable<System.DateTime> SatWorkingHoursStart { get; set; }
        public Nullable<System.DateTime> SatWorkingHoursEnd { get; set; }
        public Nullable<System.DateTime> SunWorkingHoursStart { get; set; }
        public Nullable<System.DateTime> SunWorkingHoursEnd { get; set; }




    }
}