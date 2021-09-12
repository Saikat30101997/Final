using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ImportContactModel
    {
        public string ExcelFileName { get; set; }
        public IFormFile ExcelFile { get; set; }
    }
}
