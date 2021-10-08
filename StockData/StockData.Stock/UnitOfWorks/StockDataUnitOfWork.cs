using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Stock.Contexts;
using StockData.Stock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.UnitOfWorks
{
    public class StockDataUnitOfWork : UnitOfWork,
        IStockDataUnitOfWork
    {

        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrices { get; private set; }
        public StockDataUnitOfWork(IStockDbContext context,
            ICompanyRepository companies,
            IStockPriceRepository stockPrices) : base((DbContext)context)
        {
            Companies = companies;
            StockPrices = stockPrices;
        }
    }
}
