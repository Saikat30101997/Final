using OfficeOpenXml;
using System;
using System.IO;
namespace DataImporter.Worker.ReadFunctions
{
    public class ExcelReadFunction : IExcelReaderFunction
    {
        public void ReadExcelData()
        {
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/Confirm";
            string s = null;
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.xlsx");
            foreach (FileInfo file in Files)
            {
                s = file.FullName;
                string fileName = Path.GetFileNameWithoutExtension(s);
                Guid? UserId=null;
                int? id=null;
                string str = "";
                char matchCharacter = '_';
                int j = 0;
                for(int i=0;i<fileName.Length;i++)
                {
                    if (j == 2) break;
                    char c = fileName[i];
                    if (c==matchCharacter)
                    {
                        if (j == 0)
                        {
                            UserId = Guid.Parse(str);
                            str = "";
                            j++;
                        }
                        else if (j == 1)
                        {
                            id = Convert.ToInt32(str);
                            str = "";
                            j++;
                        }
                    }
                    else
                        str = str + fileName[i];
                }
                Console.WriteLine(UserId);
                Console.WriteLine(id);
                Console.WriteLine(Path.GetFileNameWithoutExtension(s));
                readXLS(s);
            }
        }

        public void readXLS(string FilePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                //for (int row = 1; row <= rowCount; row++)
                //{
                //    for (int col = 1; col <= colCount; col++)
                //    {
                //        Console.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value?.ToString().Trim());
                //    }
                //}

                for(int col=1;col<=colCount;col++)
                {
                    for(int row=1;row<=rowCount;row++)
                    {
                        Console.WriteLine(worksheet.Cells[1, col].Value?.ToString().Trim());
                        Console.WriteLine("Row "+ row+" Column "+col+" Value "+ worksheet.Cells[row, col].Value?.ToString().Trim());
                    }
                }
            }
        }
    }
}
