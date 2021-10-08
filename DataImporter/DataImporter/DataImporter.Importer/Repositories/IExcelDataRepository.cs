using DataImporter.Data;
using DataImporter.Importer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public interface IExcelDataRepository : IRepository<ExcelData,int>
    {
    }
}
