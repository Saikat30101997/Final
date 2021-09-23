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
       

        private  IWebHostEnvironment _hostEnvironment;
        private  IGroupService _groupService;
        private ILifetimeScope _scope;
        public EditGroupModel()
        {
           
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
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
