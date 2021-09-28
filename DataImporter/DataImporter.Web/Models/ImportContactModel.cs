using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ImportContactModel
    {
        
        public int Id { get; set; }
        public string ExcelFileName { get; set; }
        public IFormFile ExcelFile { get; set; }
        public List<string> listColumn { get; set; }
        public string fileName { get; set; } 
        public List<List<string> >Values { get; set; }
        public int CountValue { get; set; } = 0;
        public int fileMatchValue { get; set; } = 0;
        public Guid UserId { get; set; }

        public int id { get; set; }
        private IWebHostEnvironment _hostEnvironment;
        private IGroupService _groupService;
        private IFileSearching _fileSearching;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IImportService _importService;
        private FilePath _filePath;
        
        public ImportContactModel()
        {
          
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _fileSearching = _scope.Resolve<IFileSearching>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _importService = _scope.Resolve<IImportService>();
        }
        public ImportContactModel(IWebHostEnvironment hostEnvironment,
            IGroupService groupService,
            IFileSearching fileSearching,IDateTimeUtility dateTimeUtility,
            IImportService importService,IOptions<FilePath>filepath)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
            _fileSearching = fileSearching;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
            _filePath = filepath.Value;
        }
        internal void Create(int id,Guid Id)
        {
            
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ExcelFile.FileName;
            var files = fileName.Split('.');
            ExcelFileName = Id.ToString() + "_" + id.ToString() + "_" + DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            string path = Path.Combine(wwwRootPath + "/EXCELS/", ExcelFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                ExcelFile.CopyTo(fileStream);
            }
           
        }

        internal void ExcelValues()
        {
           
            //  string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/EXCELS";
            string path = _filePath.ExcelsPath;
            var Files = _fileSearching.GetExcelFiles(path);
            string s = null;
            foreach (var file in Files)
            {
                s = file.FullName;
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(s);
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var fileName = _fileSearching.GetFileName(fileNamewithGuid);
                UserId = Guid.Parse(fileNameSplit[0]);
                id = Convert.ToInt32(fileNameSplit[1]);
                var listCol = new List<string>();
                
                var lists = new List<List<string>>();
                
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column; 
                    int rowCount = worksheet.Dimension.End.Row;
                    string str = "";
                    for(int col=1;col<=colCount;col++)
                    {
                        listCol.Add(worksheet.Cells[1, col].Value?.ToString().Trim());

                        if (col < colCount)
                            str = str + worksheet.Cells[1, col].Value?.ToString().Trim() + '/';
                        else if (col == colCount)
                            str = str + worksheet.Cells[1, col].Value?.ToString().Trim();
                    }
                    var importList = _importService.GetImportListData(UserId, id);
                    if (importList.Count>0)
                    {
                        if (importList[0].ColumnName != str && importList[0].ColumnName != string.Empty)
                        {
                            fileMatchValue = 1;
                            Delete();
                        }
                       
                    }

                    for (int row = 2; row <= Math.Min(10,rowCount); row++)
                    {
                        var listVal = new List<string>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            listVal.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                        }
                        lists.Add(listVal);
                    }
                    listColumn = listCol;
                  
                
                }
                Values = lists;
                if (_importService.GetStatus(fileName, UserId, id) == "Completed")
                {
                    CountValue = 1;
                    Delete();
                }

                if (fileMatchValue == 1) Delete();
            }
        }
        internal void Upload()
        {
            //  string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/EXCELS";
            string path = _filePath.ExcelsPath;
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var item in Files)
            {
                s = item.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var fileName = _fileSearching.GetFileName(fileNamewithGuid);
                UserId = Guid.Parse(fileNameSplit[0]);
                id = Convert.ToInt32(fileNameSplit[1]);
                if (_importService.GetStatus(fileNameSplit[3], UserId, id) == null
                    || _importService.GetStatus(fileNameSplit[3], UserId, id)=="Pending")
                {
                    if(_importService.GetStatus(fileNameSplit[3], UserId, id) == "Pending")
                    {
                        _importService.DeleteFile(fileNameSplit[3], UserId, id);
                        DeleteFromConfirm(fileNameSplit[3], UserId, id);
                    }
                    string str = "";
                    FileInfo existingFile = new FileInfo(s);
                    using (ExcelPackage package = new ExcelPackage(existingFile))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int colCount = worksheet.Dimension.End.Column;
                        int rowCount = worksheet.Dimension.End.Row;
                       
                        for (int col = 1; col <= colCount; col++)
                        {
                            
                            if (col < colCount)
                                str = str + worksheet.Cells[1, col].Value?.ToString().Trim() + '/';
                            else if (col == colCount)
                                str = str + worksheet.Cells[1, col].Value?.ToString().Trim();
                        }
                    }
                    var import = new Import
                    {
                        GroupId = id,
                        ExcelFileName = fileNameSplit[3],
                        UserId = UserId,
                        Status = "Pending",
                        GroupName = _importService.GetGroupName(id),
                        ImportDate = _dateTimeUtility.Now(),
                        ColumnName = str
                         
                       };
                    _importService.Create(import);
                    //  string destination = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/Confirm";
                    string destination = _filePath.ConfirmPath;
                    string fulldest = destination + "/" + Path.GetFileNameWithoutExtension(s) + ".xlsx";
                    File.Move(s, fulldest);
                }
            
            }
        }

        internal void Delete()
        {
            // string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/EXCELS";
            string path = _filePath.ExcelsPath;
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
         
                if (File.Exists(s))
                {
                    file.Delete();
                }
            }
        }

        internal void DeleteFromConfirm(string FileName,Guid userId,int id)
        {
            // string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/Confirm";
            string path = _filePath.ConfirmPath;
            string s = null;
       
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var fileName = _fileSearching.GetFileName(fileNamewithGuid);
                if (Guid.Parse(fileNameSplit[0]) == userId && 
                    Convert.ToInt32(fileNameSplit[1]) == id && fileNameSplit[3] == FileName)
                {
                    if (File.Exists(s))
                    {
                        file.Delete();
                    }
                }
            }
        }
    }
}
