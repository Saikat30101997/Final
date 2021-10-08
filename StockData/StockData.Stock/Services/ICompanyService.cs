using StockData.Stock.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.Services
{
    public interface ICompanyService
    {
        void Create(Company company);
    }
}
