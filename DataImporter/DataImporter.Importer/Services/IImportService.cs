using DataImporter.Importer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public interface IImportService
    {
        void Create(Import import);
        int GetImportId(string file1);
        void Completed(int importid);
        string GetGroupName(int id);
        (IList<Import>records,int total,int totalDisplay) GetImports(Guid id,int pageIndex, 
            int pageSize, string searchText, string sortText);
    }
}
