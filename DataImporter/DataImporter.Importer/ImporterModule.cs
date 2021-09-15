using Autofac;
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
           

            base.Load(builder);
        }
    }
}
