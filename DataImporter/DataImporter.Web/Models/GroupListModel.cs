﻿using Autofac;
using DataImporter.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class GroupListModel
    {
        private readonly IGroupService _groupService;
        public GroupListModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }
        public GroupListModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        internal object GetGroups(DataTablesAjaxRequestModel tableModel)
        {
            var data = _groupService.GetGroups(
               tableModel.PageIndex,
               tableModel.PageSize,
               tableModel.SearchText,
               tableModel.GetSortText(new string[] { "GroupName" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.GroupName,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}