using DataImporter.Importer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public interface IUpdateService
    {
        void UpdateGroupName(Group group);

        Group GetGroup(int id);
        (int groupCount,int importCount,int exportCount) GetData(Guid id);
    }
}
