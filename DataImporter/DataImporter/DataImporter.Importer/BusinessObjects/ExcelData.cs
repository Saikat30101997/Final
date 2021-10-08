﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.BusinessObjects
{
    public class ExcelData
    {
        public int Id { get; set; }
        public int ImportId { get; set; }
        public Guid UserId { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public string ExcelFileName { get; set; }
        public DateTime ImportDate { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
