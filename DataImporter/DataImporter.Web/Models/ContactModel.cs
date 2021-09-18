using Autofac;
using DataImporter.Membership.BusinessObjects;
using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ContactModel
    {
        public string GroupName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public List<string> GroupList { get; set; }
        private readonly IGroupService _groupService;
        public ContactModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public ContactModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public List<Group> GetGroups()
        {
            var group = _groupService.GetGroups();
            return group;
  
        }
    }
}
