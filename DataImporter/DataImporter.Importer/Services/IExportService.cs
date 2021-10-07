using DataImporter.Importer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public interface IExportService
    {
        void Create(Export export);
        int GetData(Guid userId, string excelFileName, string groupName);
        void Delete(int exportId);
        List<Export> GetDatabyId(int id);
        (IList<Export>records,int total,int totalDisplay) GetExport(Guid id, int pageIndex,
            int pageSize, string searchText, string sortText);
   
    }
}
