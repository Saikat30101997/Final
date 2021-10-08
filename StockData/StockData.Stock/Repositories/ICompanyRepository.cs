using StockData.Data;
using StockData.Stock.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.Repositories
{
    public interface ICompanyRepository : IRepository<Company,int>
    {
        IList<Company> GetCompanyName(Expression<Func<Company, bool>> filter);
    }
}
