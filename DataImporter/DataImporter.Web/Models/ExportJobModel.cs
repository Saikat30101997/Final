using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace DataImporter.Web.Models
{
    public class ExportJobModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        private IExcelDataService _excelDataService;
        private IImportService _importService;
        private ILifetimeScope _scope;
        private IExportService _exportService;
        private IDateTimeUtility _dateTimeUtility;
        private IFileSearching _fileSearching;
        private IExcelEmailSender _sender;
        private FilePath _filePath;

        public FileStream fileStream { get; set; }
        public int ExportId { get; set; }
        public IList<ExcelData> ExcelDatas { get; set; }

        public int CountValue { get; set; } = 0;
        public ExportJobModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _excelDataService = _scope.Resolve<IExcelDataService>();
            _importService = _scope.Resolve<IImportService>();
            _exportService = _scope.Resolve<IExportService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _fileSearching = _scope.Resolve<IFileSearching>();
            _sender = _scope.Resolve<IExcelEmailSender>();
        }
        
        public ExportJobModel(IImportService importService,
            IExcelDataService excelDataService,IExportService exportService,
            IDateTimeUtility dateTimeUtility,IFileSearching fileSearching,
            IExcelEmailSender sender,IOptions<FilePath>filePath)
        {
            _excelDataService = excelDataService;
            _importService = importService;
            _exportService = exportService;
            _dateTimeUtility = dateTimeUtility;
            _fileSearching = fileSearching;
            _sender = sender;
            _filePath = filePath.Value;
        }
        internal void LoadModelData(int id,string email)
        {
            CountValue = 0;
            ExcelDatas = _excelDataService.GetData(id);
            if (_exportService.GetData(ExcelDatas[0].UserId, ExcelDatas[0].ExcelFileName
                , ExcelDatas[0].GroupName) > 0)
            {
                int ExportId = _exportService.GetData(ExcelDatas[0].UserId, ExcelDatas[0].ExcelFileName
                , ExcelDatas[0].GroupName) ;
                Delete(ExportId, ExcelDatas[0].UserId, ExcelDatas[0].ExcelFileName, ExcelDatas[0].GroupName);
            }

            var export = new Export
            {
                GroupId = ExcelDatas[0].GroupId,
                UserId = ExcelDatas[0].UserId,
                ExcelFileName = ExcelDatas[0].ExcelFileName,
                Date = _dateTimeUtility.Now(),
                GroupName = ExcelDatas[0].GroupName
            };
            _exportService.Create(export);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var dataTable = new DataTable();
                foreach (var item in ExcelDatas)
                {

                    var splits = item.ColumnName.Split('/');
                    for (int j = 0; j < splits.Length; j++)
                    {
                        dataTable.Columns.Add(splits[j]);
                    }
                    break;

                }

                foreach (var item in ExcelDatas)
                {
                    var rows = item.ColumnValue.Split('/');
                    dataTable.Rows.Add(rows);
                }

                var workSheet = package.Workbook.Worksheets.Add("Sheet");
                workSheet.Cells.LoadFromDataTable(dataTable, true);
                package.Save();
                CountValue = 1;
                FileInfo fi = new FileInfo(/*@"G:\aspnetb5\DataImporter\DataImporter.Web\wwwroot\SaveFile\"*/ _filePath.SavePath +
                    ExcelDatas[0].UserId+'_'+ExcelDatas[0].GroupName+'_'+ExcelDatas[0].ExcelFileName + ".xlsx");
                package.SaveAs(fi);
                string path = _filePath.SavePath;
                string s = null;

                var Files = _fileSearching.GetExcelFiles(path);
                foreach (var file in Files)
                {
                    s = file.FullName;
                    string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                    var fileNameSplit = fileNamewithGuid.Split('_');
                    var filename = _fileSearching.GetFileName(fileNamewithGuid);
                    if (Guid.Parse(fileNameSplit[0]) == ExcelDatas[0].UserId && fileNameSplit[1] == ExcelDatas[0].GroupName
                    && fileNameSplit[2] == ExcelDatas[0].ExcelFileName)
                    {
                        _sender.SendEmail(email, " Excel File", $"This is your Excel file", s);
                    }
                }
            }
            
        }

        private void Delete(int exportId,Guid userId, string fileName, string groupName)
        {
            // _exportService.Delete(exportId);
            // string path = $"G:/aspnetb5/DataImporter/DataImporter.Web/wwwroot/SaveFile/";
            string path = _filePath.SavePath;
            string s = null;

            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var filename = _fileSearching.GetFileName(fileNamewithGuid);
                if(Guid.Parse(fileNameSplit[0])==userId && fileNameSplit[0]==groupName && fileNameSplit[0] == filename)
                {
                    if (File.Exists(s)) file.Delete();
                }
                
            }
        }

        public void Send(int id,string email)
        {
            var export = _exportService.GetDatabyId(id);
            string path = _filePath.SavePath;
            string s = null;

            var Files = _fileSearching.GetExcelFiles(path);
            foreach (var file in Files)
            {
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
                var filename = _fileSearching.GetFileName(fileNamewithGuid);
                if (Guid.Parse(fileNameSplit[0]) == export[0].UserId && fileNameSplit[1] == export[0].GroupName 
                    && fileNameSplit[2] == export[0].ExcelFileName)
                {
                    _sender.SendEmail(email, " Excel File", $"This is your Excel file", s);
                }
            }
        }

        public (FileStream fileStream,string mimetype,string file) Download(int id)
        {
            var export = _exportService.GetDatabyId(id);
            string path = _filePath.SavePath;
            string s = null;
            var Files = _fileSearching.GetExcelFiles(path);
            string filePath = null;
            string fileName1 = null;
            string mimeType = null;
            foreach (var file in Files)
            {
                s = file.FullName;
                string fileNamewithGuid = Path.GetFileNameWithoutExtension(s);
                var fileNameSplit = fileNamewithGuid.Split('_');
           //     var filename = _fileSearching.GetFileName(fileNamewithGuid);
                if (Guid.Parse(fileNameSplit[0]) == export[0].UserId && fileNameSplit[1] == export[0].GroupName
                    && fileNameSplit[2] == export[0].ExcelFileName)
                {
                    filePath = s;
                    fileName1 = export[0].ExcelFileName+".xlsx";
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    
                }
            }
            return (new FileStream(filePath, FileMode.Open), mimeType, fileName1);

        }
        internal object GetExports(DataTablesAjaxRequestModel tableModel, Guid id)
        {
            var data = _exportService.GetExport(id,
             tableModel.PageIndex,
             tableModel.PageSize,
             tableModel.SearchText,
             tableModel.GetSortText(new string[] { "GroupName", "ExcelFileName", "Date" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.ExcelFileName,
                                record.Date.ToString(),
                                record.Id.ToString()

                        }
                    ).ToArray()
            };
        }
    }
}
