using Autofac;
using DataImporter.Importer.Contexts;
using DataImporter.Importer.Repositories;
using DataImporter.Importer.Services;
using DataImporter.Importer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer
{
    public class ImporterModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ImporterModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImporterDbContext>().AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<ImporterDbContext>().As<IImporterDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportRepository>().As<IImportRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<ExportRepository>().As<IExportRepository>()
             .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>()
             .InstancePerLifetimeScope();
            builder.RegisterType<ImporterUnitOfWork>().As<IImporterUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportService>().As<IImportService>()
          .InstancePerLifetimeScope();
            builder.RegisterType<ExportService>().As<IExportService>()
          .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataService>().As<IExcelDataService>()
          .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
