using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public interface IFileSearching
    {
        FileInfo[] GetExcelFiles(string path);
        string GetFileName(string fileName);
    }
}
