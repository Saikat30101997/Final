using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = DataImporter.Membership.Entities;
using BO = DataImporter.Membership.BusinessObjects;

namespace DataImporter.Membership.Profiles
{
    public class DataImporterProfile : Profile
    {
        public DataImporterProfile()
        {
            CreateMap<EO.Group, BO.Group>().ReverseMap();
        }
    }
}
