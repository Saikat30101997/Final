using DataImporter.Data;
using DataImporter.Importer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.UnitOfWorks
{
    public interface IImporterUnitOfWork : IUnitOfWork
    {
        IGroupRepository Groups { get; } 
        IImportRepository Imports { get; }
        IExportRepository Exports { get; }
        IExcelDataRepository ExcelDatas { get; }
    }
}
