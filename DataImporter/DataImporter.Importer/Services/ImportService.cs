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

            _importerUnitOfWork.Save();
        }

        public List<Import> GetImportList(string file1,Guid userId,int id)
        {
            var importEntity = _importerUnitOfWork.Imports.Get(x => x.ExcelFileName == file1&&x.UserId==userId&&x.GroupId==id);
            var imports = new List<Import>();
            foreach (var item in importEntity)
            {
                var import = _mapper.Map<Import>(item);
                imports.Add(import);
            }
            return imports;
        }

        public string GetGroupName(int id)
        {
            var groups = _importerUnitOfWork.Groups.Get(x => x.Id == id);
            string name = groups[0].GroupName;
            return name;
        }
        public (IList<Import> records, int total, int totalDisplay) GetImports(Guid id,int pageIndex, int pageSize, string searchText, string sortText)
        {
            
            var importData = _importerUnitOfWork.Imports.GetDynamic(string.IsNullOrWhiteSpace(searchText)?null
                :x=>x.GroupName.Contains(searchText) || x.ImportDate.ToString().Contains(searchText), 
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from import in importData.data
                              where import.UserId == id
                              orderby import.Id descending
                              select _mapper.Map<Import>(import)).ToList();
                 

            return (resultData, importData.total, importData.totalDisplay);
        }

        public string GetStatus(string fileName, Guid userId, int id)
        {
            var imports = _importerUnitOfWork.Imports.Get(x => x.ExcelFileName == fileName 
            && x.UserId == userId && x.GroupId == id);
          
            if (imports.Count>0) return imports[0].Status;
            else return null;
        }

        public void DeleteFile(string fileName, Guid userId, int id)
        {
            var imports = _importerUnitOfWork.Imports.Get(x => x.ExcelFileName == fileName 
            && x.UserId == userId && x.GroupId == id);
            _importerUnitOfWork.Imports.Remove(imports[0].Id);
            _importerUnitOfWork.Save();
        }

        public List<Import> GetImportListData(Guid userId, int id)
        {
            var importEntity = _importerUnitOfWork.Imports.Get(x=> x.UserId == userId && x.GroupId == id);
            var imports = new List<Import>();
            foreach (var item in importEntity)
            {
                var import = _mapper.Map<Import>(item);
                imports.Add(import);
            }
            return imports;
        }

        public void Delete(int id)
        {
            _importerUnitOfWork.Imports.Remove(id);
            _importerUnitOfWork.Save();
        }
    }
}
