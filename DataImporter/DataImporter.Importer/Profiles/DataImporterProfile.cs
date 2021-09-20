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
        }
    }
}
