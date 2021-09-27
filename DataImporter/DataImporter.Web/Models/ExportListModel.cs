using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Importer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ExportListModel
    {
        private ILifetimeScope _scope;
        private IExportService _exportService;
        public ExportListModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _exportService = _scope.Resolve<IExportService>();
        }

        public ExportListModel(IExportService exportService)
        {
            _exportService = exportService;
        }

     
    }
}
