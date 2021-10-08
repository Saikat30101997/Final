using StockData.Data;
using StockData.Stock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.UnitOfWorks
{
    public interface IStockDataUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IStockPriceRepository StockPrices { get; }
    }
}
