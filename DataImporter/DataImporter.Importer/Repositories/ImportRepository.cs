using DataImporter.Data;
using DataImporter.Importer.Contexts;
using DataImporter.Importer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public class ImportRepository : Repository<Import,int> ,IImportRepository
    {
        public ImportRepository(IImporterDbContext context):base((DbContext)context)
        {

        }
        public IList<Import> Get(Expression<Func<Import, bool>> filter)
        {
            IQueryable<Import> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
    }
    
}

