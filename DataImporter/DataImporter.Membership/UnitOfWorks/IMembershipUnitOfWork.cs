using DataImporter.Data;
using DataImporter.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.UnitOfWorks
{
    public interface IMembershipUnitOfWork : IUnitOfWork
    {
        IGroupRepository Groups { get; } 
    }
}
