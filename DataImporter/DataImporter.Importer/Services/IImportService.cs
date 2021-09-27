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
        List<Import> GetImportList(string file1,Guid userId,int id);
        void Completed(int importid);
        string GetGroupName(int id);
        (IList<Import>records,int total,int totalDisplay) GetImports(Guid id,int pageIndex, 
            int pageSize, string searchText, string sortText);
        string GetStatus(string fileName, Guid userId, int id);
        void DeleteFile(string fileName, Guid userId, int id);
        List<Import> GetImportListData(Guid userId, int id);
    }
}
