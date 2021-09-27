using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public class ExportService :IExportService
    {
        private readonly IImporterUnitOfWork _importerUnitOfWork;
        private readonly IMapper _mapper;
        public ExportService(IImporterUnitOfWork importerUnitOfWork, IMapper mapper)
        {
            _importerUnitOfWork = importerUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Export export)
        {
            if (export == null)
                throw new InvalidOperationException("Export is not provided");
            _importerUnitOfWork.Exports.Add(
                _mapper.Map<Entities.Export>(export));
            _importerUnitOfWork.Save();
        }

        public void Delete(int exportId)
        {
            _importerUnitOfWork.Exports.Remove(exportId);
            _importerUnitOfWork.Save();
        }

        public int GetData(Guid userId, string excelFileName, string groupName)
        {
            var exports = _importerUnitOfWork.Exports.Get(x => x.ExcelFileName == excelFileName
            && x.GroupName == groupName && x.UserId == userId, string.Empty);
            if (exports.Count == 0) return 0;
            else return exports[0].Id;
        }

        public List<Export> GetDatabyId(int id)
        {
            List<Entities.Export> exportEntity = _importerUnitOfWork.Exports.Get(x => x.Id == 
            id, string.Empty).ToList();
            var exports =new List<Export>();
            foreach (var export in exportEntity)
            {
                var ex = _mapper.Map<Export>(export);
                exports.Add(ex);
            }
            return exports;

        }

        public (IList<Export> records, int total, int totalDisplay) GetExport(Guid id,
            int pageIndex, int pageSize, string searchText, string sortText)
        {
            var exportData = _importerUnitOfWork.Exports.GetDynamic(string.IsNullOrWhiteSpace
                (searchText) ?
                null : x => x.Date.ToString().Contains(searchText), sortText, string.Empty, pageIndex, pageSize);
            var resultData = (from export in exportData.data where export.UserId==id
                              select new Export
                              {
                                  Id = export.Id,
                                  GroupId = export.GroupId,
                                  UserId = export.UserId,
                                  Date = export.Date,
                                  ExcelFileName = export.ExcelFileName,
                                  GroupName = export.GroupName
                              }).ToList();

            return (resultData, exportData.total, exportData.totalDisplay);
        }
    }
}
