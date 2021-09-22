using DataImporter.Data;
using DataImporter.Importer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Repositories
{
    public interface IImportRepository : IRepository<Import,int>
    {
        IList<Import> Get(Expression<Func<Import, bool>> filter);
    }
}
