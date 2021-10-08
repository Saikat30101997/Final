using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = DataImporter.Importer.Entities;
using BO = DataImporter.Importer.BusinessObjects;

namespace DataImporter.Importer.Profiles
{
    public class DataImporterProfile : Profile
    {
        public DataImporterProfile()
        {
            CreateMap<EO.Group, BO.Group>().ReverseMap();
            CreateMap<EO.Import, BO.Import>().ReverseMap();
            CreateMap<EO.ExcelData, BO.ExcelData>().ReverseMap();
            CreateMap<EO.Export, BO.Export>().ReverseMap();
        }
    }
}
