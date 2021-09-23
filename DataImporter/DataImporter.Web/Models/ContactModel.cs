using Autofac;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;

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
        private  IGroupService _groupService;
        private ILifetimeScope _scope;
        public ContactModel()
        {
           
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
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
