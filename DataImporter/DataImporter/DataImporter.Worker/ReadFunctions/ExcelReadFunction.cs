using DataImporter.Common.Utilities;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using DataImporter.Worker.FilePaths;
using Microsoft.Extensions.Options;
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
        private WorkerFilePath _filePath;
        public ExcelReadFunction(IFileSearching fileSearching,
            IDateTimeUtility dateTimeUtility,IImportService importService,
            IExcelDataService excelDataService,IOptions<WorkerFilePath> filePath)
        {
            _fileSearching = fileSearching;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
            _excelDataService = excelDataService;
            _filePath = filePath.Value;
        }
        public Guid UserId { get; set; }
        public int id { get; set; }

        public List<List<string>> Excel { get; set; }
        public void ReadExcelData()
        {
            //string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/Confirm";
            string path = _filePath.ConfirmPath;
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            if (Files.Length == 0) Console.WriteLine("No file Exists");
            foreach (FileInfo file in Files)
            {
                
                s = file.FullName;
                
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var fileName = _fileSearching.GetFileName(fileNamewithGuid);
                UserId = Guid.Parse(fileNameSplit[0]);
                id = Convert.ToInt32(fileNameSplit[1]);
                var groupName = _importService.GetGroupName(id);
                var imports=_importService.GetImportList(fileName,UserId,id);
                foreach (var item in imports)
                {
                    if (item.ExcelFileName == fileName && item.UserId == UserId
                        && item.GroupId == id &&item.Status== "Pending")
                    {
                        StoreExcelData(s, item.Id, id, UserId, fileName, groupName);
                        if (File.Exists(s))
                        {
                            file.Delete();
                        }
                        Console.WriteLine("Entry Done");
                        _importService.Completed(item.Id);
                    }
                    else Console.WriteLine("Entry Already Completed");
                 
                }
               
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
                        if (col < colCount)
                            s = s + worksheet.Cells[row, col].Value?.ToString().Trim() + '/';
                        else if(col==colCount) 
                            s = s + worksheet.Cells[row, col].Value?.ToString().Trim();
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
