using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class UserSerializeModel
    {
        public int Id { get; set; }
        public string OrganisationName { get; set; }
        public string Email { get; set; }
    }
}