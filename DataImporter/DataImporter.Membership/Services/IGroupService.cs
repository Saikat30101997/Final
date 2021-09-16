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
        (IList<Group>records,int total,int totalDisplay) GetGroups(int pageIndex,
            int pageSize, string searchText, string sortText);
    }
}
