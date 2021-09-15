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
        public GroupService(IMembershipUnitOfWork membershipUnitOfWork)
        {
            _membershipUnitOfWork = membershipUnitOfWork;
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
    }
}
