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
    public class UpdateService : IUpdateService
    {
        private readonly IImporterUnitOfWork _importerUnitOfWork;
        private readonly IMapper _mapper;
        public UpdateService(IImporterUnitOfWork importerUnitOfWork, IMapper mapper)
        {
            _importerUnitOfWork = importerUnitOfWork;
            _mapper = mapper;
        }

        public (int groupCount, int importCount, int exportCount) GetData(Guid id)
        {
            int groupNumber = _importerUnitOfWork.Groups.GetCount(x => x.UserId == id);
            int importNumber = _importerUnitOfWork.Imports.GetCount(x => x.UserId == id);
            int exportNumber = _importerUnitOfWork.Exports.GetCount(x => x.UserId == id);
            return (groupNumber, importNumber, exportNumber);
        }

        public Group GetGroup(int id)
        {
            var group = _importerUnitOfWork.Groups.GetById(id);
            return _mapper.Map<Group>(group);

        }

        public void UpdateGroupName(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is not provided");
            var groupEntity = _importerUnitOfWork.Groups.GetById(group.Id);
            var importEntity = _importerUnitOfWork.Imports.Get(x => x.GroupId == group.Id);
            var excelDataEntity = _importerUnitOfWork.ExcelDatas.Get(x => x.GroupId == group.Id, string.Empty);
            var exportDataEntity = _importerUnitOfWork.Exports.Get(x => x.GroupId == group.Id, string.Empty);
            if (groupEntity != null)
            {
                groupEntity.GroupName = group.GroupName;

                if (importEntity.Count != 0)
                {
                    foreach (var item in importEntity)
                    {
                        item.GroupName = group.GroupName;

                    }
                }
                if (excelDataEntity.Count != 0)
                {
                    foreach (var item in excelDataEntity)
                    {
                        item.GroupName = group.GroupName;

                    }
                }
                if (exportDataEntity.Count != 0)
                {
                    foreach (var item in exportDataEntity)
                    {
                        item.GroupName = group.GroupName;

                    }
                }

                _importerUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Group is not updated");
        }


    }
}
