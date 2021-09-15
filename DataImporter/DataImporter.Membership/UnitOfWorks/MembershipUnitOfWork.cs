using DataImporter.Data;
using DataImporter.Membership.Contexts;
using DataImporter.Membership.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.UnitOfWorks
{
    public class MembershipUnitOfWork : UnitOfWork, IMembershipUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public MembershipUnitOfWork(IApplicationDbContext context,
            IGroupRepository groups):base((DbContext)context)
        {
            Groups = groups;
        }
    }
}
