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
    public class CreateImportModel
    {
        public string Name { get; set; }

        private  IGroupService _groupService;
        private ILifetimeScope _scope;
        public CreateImportModel()
        {
            
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
        }
        public CreateImportModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public void Create(Guid id)
        {
            var group = new Group
            {
                GroupName = Name,
                UserId = id
            };

            _groupService.Create(group);
        }
    }
}
