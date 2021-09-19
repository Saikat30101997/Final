using Autofac;
using DataImporter.Membership.BusinessObjects;
using DataImporter.Membership.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IGroupService _groupService;
        
        public ImportContactModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _hostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
        }
        public ImportContactModel(IWebHostEnvironment hostEnvironment,
            IGroupService groupService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;

        }
        internal void Create(int id,Guid Id)
        {
          
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ExcelFile.FileName;
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
            string s = null;
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.xlsx");
            foreach (var file in Files)
            {
                s = file.FullName;
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo existingFile = new FileInfo(s);
                var listCol = new List<string>();
                
                var lists = new List<List<string>>();
                
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    CountValue = 1;
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count
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
                   
                }
                Values = lists;
            }

          

            
        }
    }
}
