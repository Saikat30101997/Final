using DataImporter.Worker.ReadFunctions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IExcelReaderFunction _excelReadFunction;

        public Worker(ILogger<Worker> logger, IExcelReaderFunction excelReaderFunction)
        {
            _logger = logger;
            _excelReadFunction = excelReaderFunction;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _excelReadFunction.ReadExcelData();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
