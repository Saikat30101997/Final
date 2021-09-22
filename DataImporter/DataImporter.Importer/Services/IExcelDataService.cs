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
    }
}
