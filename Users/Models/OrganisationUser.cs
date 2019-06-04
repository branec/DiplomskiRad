using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Users.Models
{
    public class OrganisationUser : MembershipUser
    {
        #region User Properties  

        public int Id { get; set; }
        public string OrganisationName { get; set; }
        public string LastName { get; set; }

        #endregion

        public OrganisationUser(Organisation user) : base("UserProvider", user.Username, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            Id = user.Id;
            OrganisationName = user.Name;
 
        }
    }
}