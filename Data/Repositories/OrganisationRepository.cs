using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisationRepository
    {
        public OrganisationRepository(Entities Entities) : base(Entities)
        {
        }

        public Organisation GetOrganisationByUsername(string Username)
        {
            return Entities.Organisations.SingleOrDefault(o => o.Username == Username);
        }

        public Organisation GetOrganisationByEmail(string Email)
        {
            return Entities.Organisations.SingleOrDefault(o => o.Email == Email);
        }

        public Organisation GetOrganisationByUsernamePassword(string Username, string Password)
        {
            return Entities.Organisations.SingleOrDefault(o => o.Username == Username && o.Password == Password);
        }

        public Organisation GetOrganisationByActivationCode(string activationCode)
        {
            return Entities.Organisations.SingleOrDefault(o => o.ActivationCode.Value.ToString() == activationCode);
        }

        public Entities Entities
        {
            get { return Context as Entities; }
        }
    }
}
