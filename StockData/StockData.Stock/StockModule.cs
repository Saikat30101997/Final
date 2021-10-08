using Autofac;
using StockData.Stock.Contexts;
using StockData.Stock.Repositories;
using StockData.Stock.Services;
using StockData.Stock.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock
{
    public class StockModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public StockModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDbContext>().As<IStockDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<StockDataUnitOfWork>().As<IStockDataUnitOfWork>()
              .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
               .InstancePerLifetimeScope();



            base.Load(builder);
        }
    }
}
