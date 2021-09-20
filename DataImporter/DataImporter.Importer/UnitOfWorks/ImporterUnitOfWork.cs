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
        public ImporterUnitOfWork(IImporterDbContext context,
            IGroupRepository groups):base((DbContext)context)
        {
            Groups = groups;
        }
    }
}
