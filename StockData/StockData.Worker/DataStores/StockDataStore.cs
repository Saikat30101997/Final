using HtmlAgilityPack;
using StockData.Stock.BusinessObjects;
using StockData.Stock.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockData.Worker.DataStores
{
    public class StockDataStore : IStockDataStore
    {
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;
        public StockDataStore(ICompanyService companyService,
            IStockPriceService stockPriceService)
        {
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }
      
       public void GetStockData()
       {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.dse.com.bd/latest_share_price_scroll_l.php");

            var nodes = document.DocumentNode.SelectNodes("//div[@class='table-responsive inner-scroll']" +
                "/table[contains(@class, " +
                "'table table-bordered background-white shares-table fixedHeader')]//td").ToArray();

            List<string> Nodes = new List<string>();

            foreach (var node in nodes)
            {
                Nodes.Add(ConvertHtmlNodeToString(node));
            }
            var marketStatus = nodes.FirstOrDefault().
                SelectSingleNode("//span/span").InnerText;


            //var company = new Company();
            //for (int i = 0; i < Nodes.Count; i++)
            //{
            //    if (i % 11 == 1)
            //    {
            //        company.TradeCode = Nodes[i];
            //        _companyService.Create(company);
            //    }

            //}


            if (marketStatus == "Open")
            {

                var stockPrice = new StockPrice();
                Console.WriteLine("Welcome To Dhaka Stock Exchange");
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (i % 11 == 0 && i != 0)
                    {
                        _stockPriceService.Create(stockPrice);
                    }
                    if (i % 11 == 1)
                        stockPrice.Tradecode = Nodes[i];
                    if (i % 11 == 2)
                        stockPrice.LastTradingPrice = Nodes[i];
                    if (i % 11 == 3)
                        stockPrice.High = Nodes[i];
                    if (i % 11 == 4)
                        stockPrice.Low = Nodes[i];
                    if (i % 11 == 5)
                        stockPrice.ClosePrice = Nodes[i];
                    if (i % 11 == 6)
                        stockPrice.YesterdayClosePrice = Nodes[i];
                    if (i % 11 == 7)
                        stockPrice.Change = Nodes[i];
                    if (i % 11 == 8)
                        stockPrice.Trade = Nodes[i];
                    if (i % 11 == 9)
                        stockPrice.Value = Nodes[i];
                    if (i % 11 == 10)
                        stockPrice.Volume = Nodes[i];
                }

                _stockPriceService.Create(stockPrice);
            }
            else
            {
                Console.WriteLine("Market Closed");
            }
       }
       private string ConvertHtmlNodeToString(HtmlNode node)=>
            Regex.Replace(node.InnerText, @"\s", "");
        
    }
}
