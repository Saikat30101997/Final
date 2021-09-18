using AutoMapper;
using DataImporter.Membership.BusinessObjects;
using DataImporter.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMembershipUnitOfWork _membershipUnitOfWork;
        private readonly IMapper _mapper;
        public GroupService(IMembershipUnitOfWork membershipUnitOfWork,IMapper mapper)
        {
            _membershipUnitOfWork = membershipUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is not Provided");
            _membershipUnitOfWork.Groups.Add(
                new Entities.Group
                {
                    GroupName = group.GroupName,
                    UserId = group.UserId
                });
            _membershipUnitOfWork.Save();
        }

        public Group GetGroup(int id)
        {
            var group = _membershipUnitOfWork.Groups.GetById(id);
            return new Group
            {
                Id = group.Id,
                UserId = group.UserId,
                GroupName = group.GroupName
            };
        }

        public List<Group> GetGroups()
        {
            var groupEntity = _membershipUnitOfWork.Groups.GetAll();
            var groups =new  List<Group>();
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

        public (IList<Group> records, int total, int totalDisplay) GetGroups(Guid id,int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            var groupData = _membershipUnitOfWork.Groups.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? 
                null : x => x.GroupName.Contains(searchText), sortText, 
                string.Empty, pageIndex, pageSize);

            var resultData = (from grp in groupData.data where grp.UserId==id
                              select _mapper.Map<Group>(grp)).ToList();


            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public void UpdateGroupName(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is not provided");
            var groupEntity = _membershipUnitOfWork.Groups.GetById(group.Id);
            if(groupEntity!=null)
            {
                groupEntity.GroupName = group.GroupName;
                _membershipUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Group is not updated");
        }
    }
}
