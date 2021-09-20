using Autofac;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class EditGroupModel
    {
        public int id { get; set; }
        public string Name { get; set; }
       

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IGroupService _groupService;
        public EditGroupModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public EditGroupModel(IWebHostEnvironment hostEnvironment,
            IGroupService groupService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;

        }

        internal void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            id = group.Id;
            Name = group.GroupName;
        }

        internal void Update()
        {
            var group = new Group
            {
                Id = id,
                GroupName = Name
            };
            _groupService.UpdateGroupName(group);
        }
    }
}
