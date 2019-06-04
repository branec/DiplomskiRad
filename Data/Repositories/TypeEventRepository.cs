using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TypeEventRepository : Repository<TypeEvent>, ITypeEventRepository
    {
        public TypeEventRepository(Entities Entities) : base(Entities)
        {
        }
        public Entities Entities
        {
            get { return Context as Entities; }
        }
    }
}
