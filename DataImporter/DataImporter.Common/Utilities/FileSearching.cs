using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class FileSearching : IFileSearching
    {
        public FileInfo[] GetExcelFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.xlsx");
            return Files;
        }
    }
}
