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

        public string GetFileName(string fileName)
        {
            string name = "";
            int counter = 0;
            for(int i=0;i<fileName.Length;i++)
            {
                if (fileName[i] == '.') break;
                if (fileName[i] == '_') 
                { 
                    counter++; i++; 
                }
                if (counter == 3) name = name + fileName[i];
            }
            return name;
        }

    }
}
