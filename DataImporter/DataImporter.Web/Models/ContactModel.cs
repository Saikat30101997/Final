using Autofac;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;

using DataImporter.Membership.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ContactModel
    {
        [Required]
        public string GroupName { get; set; }
        [Required]
        public DateTime? DateFrom { get; set; }
        [Required]
        public DateTime? DateTo { get; set; }
        public List<Group> Groups { get; set; }
        public IEnumerable<SelectListItem> GroupList { get; set; }
        public List<string> Columns { get; set; }
        public List<List<string>> Values { get; set; }
        public List<ExcelData> GetAll { get; set; }

        private  IGroupService _groupService;
        private IExcelDataService _excelDataService;
        private ILifetimeScope _scope;
        public string SelectedGroupText { get; set; }
        public int CountValue { get; set; } = 0;
        public ContactModel()
        {
           
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupService = _scope.Resolve<IGroupService>();
            _excelDataService = _scope.Resolve<IExcelDataService>();
        }
        public ContactModel(IGroupService groupService,IExcelDataService excelDataService)
        {
            _groupService = groupService;
            _excelDataService = excelDataService;
        }
        public void GetGroups(Guid Id)
        {
            Groups = _groupService.GetGroups(Id);
            GroupList = Groups.Select(x=>new SelectListItem { 
               Text = x.GroupName,
               Value = x.GroupName
            }).ToList();
        }

        internal void GetContactsData(Guid Id,string groupName, DateTime dateFrom, DateTime dateTo)
        {
            dateFrom = DateFrom.HasValue ? DateFrom.Value : DateTime.MinValue;
            dateTo = DateTo.HasValue ? DateTo.Value : DateTime.MaxValue;
            GetAll = _excelDataService.GetAllData(Id,groupName,dateFrom,dateTo);

            if (GetAll.Count > 0)
            {
                CountValue = 1;
                var data = new List<string>();
                foreach (var item in GetAll)
                {
                    var splits = item.ColumnName.Split('/');
                    foreach (var item1 in splits)
                    {
                        data.Add(item1);
                    }
                    break;
                }
                var lists = new List<List<string>>();
                foreach (var item in GetAll)
                {
                    var splits = item.ColumnValue.Split('/');
                    var list = new List<string>();
                    foreach (var item1 in splits)
                    {
                        list.Add(item1);
                    }
                    lists.Add(list);
                }
                Columns = data;
                Values = lists;
            }
            else CountValue = 0;
        }
    }
}
