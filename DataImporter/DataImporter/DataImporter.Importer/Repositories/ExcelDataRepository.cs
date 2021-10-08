using DataImporter.Data;
using DataImporter.Importer.Contexts;
using DataImporter.Importer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public class ExcelDataRepository :Repository<ExcelData,int> ,IExcelDataRepository
    {
        public ExcelDataRepository(IImporterDbContext context):base((DbContext)context)
        {

        }
    }
}
