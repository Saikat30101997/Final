using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Importer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ImportJobModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        private ILifetimeScope _scope;
        private IImportService _importService;
        public ImportJobModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _importService = _scope.Resolve<IImportService>();
        }
        public ImportJobModel(IImportService importService)
        {
            _importService = importService;
        }
        internal object GetImports(DataTablesAjaxRequestModel tableModel, Guid id)
        {
            var data = _importService.GetImports(id,
             tableModel.PageIndex,
             tableModel.PageSize,
             tableModel.SearchText,
             tableModel.GetSortText(new string[] { "ExcelFileName", "GroupName", "ImportDate", "Status" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.ExcelFileName,
                                record.GroupName,
                                record.ImportDate.ToString(),
                                record.Status,
                                record.Id.ToString()
                                
                        }
                    ).ToArray()
            };
        }
    }
}
