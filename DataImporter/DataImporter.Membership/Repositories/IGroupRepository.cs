using DataImporter.Data;
using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Repositories
{
    public  interface IGroupRepository : IRepository<Group,int >
    {
    }
}
