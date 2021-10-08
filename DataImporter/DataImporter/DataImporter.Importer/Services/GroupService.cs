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
                _mapper.Map<Entities.Group>(group));
            _importerUnitOfWork.Save();
        }

        public void Delete(int id)
        {
            _importerUnitOfWork.Groups.Remove(id);
            _importerUnitOfWork.Save();
        }

        public List<Group> GetGroups(Guid Id)
        {
            IList<Entities.Group> groupEntity = _importerUnitOfWork.Groups.GetAll();
            var groups = new List<Group>();
            var resultData = (from grp in groupEntity where grp.UserId == Id select grp).ToList();
            foreach (var group in resultData)
            {
                var gr = new Group()
                {
                    GroupName = group.GroupName
                };
                groups.Add(gr);
            }
            if (groups == null) return null;
            else return groups;

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

     
    }
}
