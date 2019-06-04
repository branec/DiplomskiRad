using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Users.Autentication
{
    public class UserPrincipal : IPrincipal
    {
        public int UserId { get; set; }
        public string OrganisationName { get; set; }
        public string Email { get; set; }

        public IIdentity Identity
        {
            get; private set;
        }

        public UserPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}