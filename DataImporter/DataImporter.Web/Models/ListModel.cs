using Autofac;
using DataImporter.Importer.Services;
using System;

namespace DataImporter.Web.Models
{
    public class ListModel
    {
        private ILifetimeScope _scope;
        private IUpdateService _updateService;

        public int GroupNumber { get; set; }
        public int ImportNumber { get; set; }
        public int ExportNumber { get; set; }
        public ListModel(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _updateService = _scope.Resolve<IUpdateService>();
        }

        public void GetData(Guid Id)
        {
            var data = _updateService.GetData(Id);
            GroupNumber = data.groupCount;
            ImportNumber = data.importCount;
            ExportNumber = data.exportCount;
        }
    }
}
