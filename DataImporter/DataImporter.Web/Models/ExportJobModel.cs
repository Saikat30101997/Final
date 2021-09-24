using Autofac;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ExportJobModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        private IExcelDataService _excelDataService;
        private IImportService _importService;
        private ILifetimeScope _scope;

        public IList<ExcelData> ExcelDatas { get; set; }
        public ExportJobModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _excelDataService = _scope.Resolve<IExcelDataService>();
            _importService = _scope.Resolve<IImportService>();
        }
        
        public ExportJobModel(IImportService importService,IExcelDataService excelDataService)
        {
            _excelDataService = excelDataService;
            _importService = importService;
        }
        internal void LoadModelData(int id)
        {
            ExcelDatas = _excelDataService.GetData(id);
        }
    }
}
