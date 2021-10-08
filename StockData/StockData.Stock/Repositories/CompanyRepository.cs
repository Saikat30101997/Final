using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Stock.Contexts;
using StockData.Stock.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.Repositories
{
    public class CompanyRepository : Repository<Company,int> ,
        ICompanyRepository
    {
        public CompanyRepository(IStockDbContext context):base((DbContext)context)
        {

        }
        public IList<Company> GetCompanyName(Expression<Func<Company, bool>> filter)
        {
            IQueryable<Company> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }


    }
}
