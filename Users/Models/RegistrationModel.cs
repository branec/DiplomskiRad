using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Users.Models
{
    public class RegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string OrganisationName { get; set; }
        public Guid ActivationCode { get; set; }
        public int OrganisationTypeId { get; set; }
        public IEnumerable<SelectListItem> OrganisationTypes { get; set; }

    }
}