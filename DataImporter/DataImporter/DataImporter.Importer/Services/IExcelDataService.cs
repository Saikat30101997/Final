using DataImporter.Importer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public interface IExcelDataService
    {
        void Create(ExcelData excelData);
        IList<ExcelData> GetData(int id);
        List<ExcelData> GetAllData(Guid id, string groupName, DateTime dateFrom, DateTime dateTo);
    }
}
