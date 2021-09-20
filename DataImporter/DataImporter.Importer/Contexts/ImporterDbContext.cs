using DataImporter.Importer.Entities;
using DataImporter.Membership.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Contexts
{
    public class ImporterDbContext : DbContext,IImporterDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ImporterDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //fluent api
        {
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations())
                .HasMany<Group>()
                .WithOne(a => a.User);
            base.OnModelCreating(modelBuilder);
        }

    }
}
