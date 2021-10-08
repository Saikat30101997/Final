using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.ReadFunctions
{
    public interface IExcelReaderFunction
    {
        void ReadExcelData();
        void StoreExcelData(string FilePath, int importId, int GroupId, Guid UserId,string fileName,string groupName);
    }
}
