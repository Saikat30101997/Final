using StockData.Stock.BusinessObjects;
using StockData.Stock.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Stock.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly IStockDataUnitOfWork _stockDataUnitOfWork;
        public StockPriceService(IStockDataUnitOfWork stockDataUnitOfWork)
        {
            _stockDataUnitOfWork = stockDataUnitOfWork;
        }

        public void Create(StockPrice stockPrice)
        {
            if (stockPrice == null)
                throw new InvalidOperationException("StockPrice is not Provided");
            _stockDataUnitOfWork.StockPrices.Add(
                new Entities.StockPrice
                {
                    CompanyId = GetId(stockPrice.Tradecode),
                    LastTradingPrice = stockPrice.LastTradingPrice,
                    High = stockPrice.High,
                    Low = stockPrice.Low,
                    ClosePrice = stockPrice.ClosePrice,
                    YesterdayClosePrice = stockPrice.YesterdayClosePrice,
                    Change = stockPrice.Change,
                    Trade = stockPrice.Trade,
                    Value = stockPrice.Value,
                    Volume = stockPrice.Volume
                });
          
            _stockDataUnitOfWork.Save();
        }

        public int GetId(string name)
        {
            var company = _stockDataUnitOfWork.Companies.GetCompanyName(x => x.TradeCode == name);
            int id = company[0].Id;
            return id;
        }
    }
}
