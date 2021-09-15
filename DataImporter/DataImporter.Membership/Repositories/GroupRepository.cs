using DataImporter.Data;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Repositories
{
    public class GroupRepository : Repository<Group,int> ,IGroupRepository
    {
        public GroupRepository(IApplicationDbContext context) : base((DbContext)context)
        {

        }
    }
}
