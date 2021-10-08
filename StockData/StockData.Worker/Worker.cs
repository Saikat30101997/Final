using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockData.Stock.BusinessObjects;

using StockData.Worker.DataStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStockDataStore _stockDataStore;
      
        public Worker(ILogger<Worker> logger,IStockDataStore stockDataStore)
        {
            _logger = logger;
            _stockDataStore = stockDataStore;
         
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _stockDataStore.GetStockData();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
