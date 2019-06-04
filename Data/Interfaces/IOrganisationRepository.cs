using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Interfaces
{
    public interface IOrganisationRepository: IRepository<Organisation>
    {
        Organisation GetOrganisationByUsername(string Username);

        Organisation GetOrganisationByEmail(string Email);

        Organisation GetOrganisationByUsernamePassword(string Username, string Password);

        Organisation GetOrganisationByActivationCode(string activationCode);

    }
}
