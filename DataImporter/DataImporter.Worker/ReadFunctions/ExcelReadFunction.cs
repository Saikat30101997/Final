using DataImporter.Common.Utilities;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
namespace DataImporter.Worker.ReadFunctions
{
    public class ExcelReadFunction : IExcelReaderFunction
    {
        private readonly IFileSearching _fileSearching;
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly IImportService _importService;
        private readonly IExcelDataService _excelDataService;
        public ExcelReadFunction(IFileSearching fileSearching,
            IDateTimeUtility dateTimeUtility,IImportService importService,
            IExcelDataService excelDataService)
        {
            _fileSearching = fileSearching;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
            _excelDataService = excelDataService;
        }
        public Guid UserId { get; set; }
        public int id { get; set; }

        public List<List<string>> Excel { get; set; }
        public void ReadExcelData()
        {
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/Confirm";
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);

            foreach (FileInfo file in Files)
            {
                
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var fileName = _fileSearching.GetFileName(fileNamewithGuid);
                UserId = Guid.Parse(fileNameSplit[0]);
                id = Convert.ToInt32(fileNameSplit[1]);
                var groupName = _importService.GetGroupName(id);
                var importid=_importService.GetImportId(fileName);
                StoreExcelData(s,importid,id,UserId,fileName,groupName);
                if (File.Exists(s))
                {
                    file.Delete();
                }
                Console.WriteLine("Entry Done");
                _importService.Completed(importid);
            }
        }

        public void StoreExcelData(string FilePath,int importId,int GroupId,Guid UserId,string fileName,string groupName)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
           

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
               
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  
                int rowCount = worksheet.Dimension.End.Row;
                var list = new List<string>();
                for (int row = 1; row <= rowCount; row++)
                {
                    string s = "";
                    for (int col = 1; col <= colCount; col++)
                    {
                        s = s + worksheet.Cells[row, col].Value?.ToString().Trim() + '/';
                    }
                    list.Add(s);
                }
                

                for (int i = 1; i < list.Count; i++)
                {
                    var ExcelData = new ExcelData
                    {
                        ImportId = importId,
                        UserId = UserId,
                        GroupId = GroupId,
                        ImportDate = _dateTimeUtility.Now(),
                        ExcelFileName = fileName,
                        ColumnName = list[0],
                        ColumnValue = list[i],
                        GroupName = groupName
                    };
                    _excelDataService.Create(ExcelData);
                }
            }
           
        }
    }
}
