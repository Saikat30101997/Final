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
        public int Id { get; set; }
        public string Name { get; set; }
       

        private  IWebHostEnvironment _hostEnvironment;
        private  IGroupService _groupService;
        private ILifetimeScope _scope;

        private IUpdateService _updateService;
        public EditGroupModel()
        {
           
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _updateService = _scope.Resolve<IUpdateService>();
        }
        public EditGroupModel(IWebHostEnvironment hostEnvironment,
            IGroupService groupService,IUpdateService updateService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
            _updateService = updateService;
            
        }

        public void LoadModelData(int id)
        {
            var group = _updateService.GetGroup(id);
            Id = group.Id;
            Name = group.GroupName;
        }

        public void Update()
        {
            var group = new Group
            {
                Id = Id,
                GroupName = Name
            };
            _updateService.UpdateGroupName(group);
        }
    }
}
