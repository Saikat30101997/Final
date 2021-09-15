using DataImporter.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Services
{
    public interface IGroupService
    {
        void Create(Group group);
        List<Group> GetGroup();
    }
}
