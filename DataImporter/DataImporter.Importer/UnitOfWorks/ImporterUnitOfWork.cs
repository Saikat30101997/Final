using DataImporter.Data;
using DataImporter.Importer.Contexts;
using DataImporter.Importer.Repositories;
using DataImporter.Importer.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.UnitOfWorks
{
    public class ImporterUnitOfWork : UnitOfWork, IImporterUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public IImportRepository Imports { get; private set; }
        public IExportRepository Exports { get; private set; }
        public IExcelDataRepository ExcelDatas { get; private set; }
        public ImporterUnitOfWork(IImporterDbContext context,
            IGroupRepository groups,IImportRepository imports,
            IExportRepository exports,
            IExcelDataRepository excelDatas):base((DbContext)context)
        {
            Groups = groups;
            Imports = imports;
            Exports = exports;
            ExcelDatas = excelDatas;
        }
    }
}
