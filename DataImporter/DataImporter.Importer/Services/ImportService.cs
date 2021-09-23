using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using EO=DataImporter.Importer.Entities;
using DataImporter.Importer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public class ImportService : IImportService
    {
        private readonly IImporterUnitOfWork _importerUnitOfWork;
        private readonly IMapper _mapper;
        public ImportService(IImporterUnitOfWork importerUnitOfWork, IMapper mapper)
        {
            _importerUnitOfWork = importerUnitOfWork;
            _mapper = mapper;
        }

        public void Completed(int importid)
        {
            var importEntity = _importerUnitOfWork.Imports.GetById(importid);
            if (importEntity != null)
            {
                importEntity.Status = "Completed";
                _importerUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Import is not found");
        }

        public void Create(Import import)
        {
            if (import == null)
                throw new InvalidOperationException("Import is not provided");
            _importerUnitOfWork.Imports.Add(
                _mapper.Map<EO.Import>(import));
            //_importerUnitOfWork.Imports.Add(
            //  new Entities.Import
            //  {
            //      ExcelFileName = import.ExcelFileName,
            //      GroupId = import.GroupId,
            //      UserId = import.UserId,
            //      Status = import.Status,
            //      ImportDate = import.ImportDate
            //  });

            _importerUnitOfWork.Save();
        }

        public int GetImportId(string file1)
        {
            var imports = _importerUnitOfWork.Imports.Get(x => x.ExcelFileName == file1);
            int id = imports[0].Id;
            return id;
        }

        public string GetGroupName(int id)
        {
            var groups = _importerUnitOfWork.Groups.Get(x => x.Id == id);
            string name = groups[0].GroupName;
            return name;
        }
        public (IList<Import> records, int total, int totalDisplay) GetImports(Guid id,int pageIndex, int pageSize, string searchText, string sortText)
        {
            
            var importData = _importerUnitOfWork.Imports.GetDynamic(string.IsNullOrWhiteSpace(searchText)?null:x=>x.GroupName.Contains(searchText), sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from import in importData.data
                              where import.UserId == id
                              select new Import
                              {
                                  Id = import.Id,
                                  ExcelFileName = import.ExcelFileName,
                                  GroupName = import.GroupName,
                                  ImportDate = import.ImportDate,
                                  Status = import.Status,
                                  GroupId = import.GroupId,
                              }).ToList();

            return (resultData, importData.total, importData.totalDisplay);
        }
    }
}
