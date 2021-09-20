using DataImporter.Data;
using DataImporter.Importer.Contexts;
using DataImporter.Importer.Entities;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public class GroupRepository : Repository<Group,int> ,IGroupRepository
    {
        public GroupRepository(IImporterDbContext context) : base((DbContext)context)
        {

        }
    }
}
