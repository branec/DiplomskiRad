using Data.Models;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TypeOrganisationRepository : Repository<TypeOrganisation> , ITypeOrganisationRepository
    {
        public TypeOrganisationRepository(Entities Entities) : base(Entities)
        {
        }
        public Entities Entities
        {
            get { return Context as Entities; }
        }

    }
}
