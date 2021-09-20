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
    public class GroupService : IGroupService
    {
        private readonly IImporterUnitOfWork _importerUnitOfWork;
        private readonly IMapper _mapper;
        public GroupService(IImporterUnitOfWork importerUnitOfWork, IMapper mapper)
        {
            _importerUnitOfWork = importerUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is not Provided");
            _importerUnitOfWork.Groups.Add(
                new Entities.Group
                {
                    GroupName = group.GroupName,
                    UserId = group.UserId
                });
            _importerUnitOfWork.Save();
        }

        public Group GetGroup(int id)
        {
            var group = _importerUnitOfWork.Groups.GetById(id);
            return new Group
            {
                Id = group.Id,
                UserId = group.UserId,
                GroupName = group.GroupName
            };
        }

        public List<Group> GetGroups()
        {
            var groupEntity = _importerUnitOfWork.Groups.GetAll();
            var groups = new List<Group>();
            foreach (var group in groupEntity)
            {
                var gr = new Group()
                {
                    GroupName = group.GroupName
                };
                groups.Add(gr);
            }
            return groups;

        }

        public (IList<Group> records, int total, int totalDisplay) GetGroups(Guid id, int pageIndex,
            int pageSize, string searchText, string sortText)
        {
            var groupData = _importerUnitOfWork.Groups.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.GroupName.Contains(searchText), sortText,
                string.Empty, pageIndex, pageSize);

            var resultData = (from grp in groupData.data
                              where grp.UserId == id
                              select _mapper.Map<Group>(grp)).ToList();


            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public void UpdateGroupName(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is not provided");
            var groupEntity = _importerUnitOfWork.Groups.GetById(group.Id);
            if (groupEntity != null)
            {
                groupEntity.GroupName = group.GroupName;
                _importerUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Group is not updated");
        }
    }
}
