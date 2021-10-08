using DataImporter.Data;
using DataImporter.Importer.Entities;
using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public  interface IGroupRepository : IRepository<Group,int >
    {
        IList<Group> Get(Expression<Func<Group, bool>> filter);
    }
}
