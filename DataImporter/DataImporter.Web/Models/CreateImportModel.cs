using Autofac;
using DataImporter.Membership.BusinessObjects;
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

        private readonly IGroupService _groupService;
        public CreateImportModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public CreateImportModel(IGroupService groupService)
        {
            _groupService = groupService;
        }
        internal void Create(Guid id)
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
