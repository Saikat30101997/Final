using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
        public Guid UserId { get; set; }

        public int id { get; set; }
        private IWebHostEnvironment _hostEnvironment;
        private IGroupService _groupService;
        private IFileSearching _fileSearching;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IImportService _importService;
        
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
            IImportService importService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
            _fileSearching = fileSearching;
            _dateTimeUtility = dateTimeUtility;
            _importService = importService;
        }
        internal void Create(int id,Guid Id)
        {
            
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ExcelFile.FileName;
            var files = fileName.Split('.');
            if (_importService.GetStatus(files[0], Id, id) == null && _importService.GetStatus(files[0], Id, id) != "Completed" && _importService.GetStatus(fileName, UserId, id) != "Pending")
            {
               
                var import = new Import
                {
                    GroupId = id,
                    ExcelFileName = files[0],
                    UserId = Id,
                    Status = "Pending",
                    GroupName = _importService.GetGroupName(id)
                };
                _importService.Create(import);
            }
            ExcelFileName = Id.ToString() + "_" + id.ToString() + "_" + DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            string path = Path.Combine(wwwRootPath + "/EXCELS/", ExcelFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                ExcelFile.CopyTo(fileStream);
            }
           
        }

        internal void ExcelValues()
        {
           
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/EXCELS";
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
                    for(int col=1;col<=colCount;col++)
                    {
                        listCol.Add(worksheet.Cells[1, col].Value?.ToString().Trim());
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
                   if(_importService.GetStatus(fileName,UserId,id)=="Pending")
                   {
                        DeleteFromConfirm();
                        Upload();
                   }
                   else if(_importService.GetStatus(fileName,UserId,id)=="Completed")
                   {
                        CountValue = 1;
                        Delete();
                    }
                }
                Values = lists;
            }
        }
        internal void Upload()
        {
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/EXCELS";
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var item in Files)
            {
                s = item.FullName;
                string destination = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/Confirm";
                string fulldest = destination +"/"+Path.GetFileNameWithoutExtension(s)+".xlsx";
                File.Move(s, fulldest);
            }
        }

        internal void Delete()
        {
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/EXCELS";
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                if(File.Exists(s))
                {
                    file.Delete();
                }
            }
        }

        internal void DeleteFromConfirm()
        {
            string path = $"G:/Final/DataImporter/DataImporter.Web/wwwroot/Confirm";
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                if (File.Exists(s))
                {
                    file.Delete();
                }
            }
        }
    }
}
