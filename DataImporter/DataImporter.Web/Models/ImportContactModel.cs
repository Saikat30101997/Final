﻿using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models
{
    public class ImportContactModel
    {
        public int Id { get; set; }
        public string ExcelFileName { get; set; }
        public IFormFile ExcelFile { get; set; }

        private readonly IWebHostEnvironment _hostEnvironment;
        
        public ImportContactModel()
        {
            _hostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
        }
        public ImportContactModel(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;

        }
        internal void Create(int id,Guid Id)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ExcelFile.FileName;
            // ExcelFileName = DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            ExcelFileName = Id.ToString() + "_" + id.ToString() + "_" + DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            string path = Path.Combine(wwwRootPath + "/EXCELS/", ExcelFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                ExcelFile.CopyTo(fileStream);
            }
        }
    }
}
