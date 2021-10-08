using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Worker.DataStores
{
    public interface IStockDataStore
    {
        void GetStockData();
    }
}
