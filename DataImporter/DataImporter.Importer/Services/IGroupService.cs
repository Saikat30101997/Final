using DataImporter.Importer.BusinessObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public interface IGroupService
    {
        void Create(Group group);
       List<Group> GetGroups();
        (IList<Group>records,int total,int totalDisplay) GetGroups(Guid id,int pageIndex,
            int pageSize, string searchText, string sortText);
        Group GetGroup(int id);
        void UpdateGroupName(Group group);
    }
}
