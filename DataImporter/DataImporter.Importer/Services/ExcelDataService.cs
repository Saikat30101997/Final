using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importer.Services
{
    public class ExcelDataService : IExcelDataService
    {
        private readonly IImporterUnitOfWork _importerUnitOfWork;
        private readonly IMapper _mapper;
        public ExcelDataService(IImporterUnitOfWork importerUnitOfWork, IMapper mapper)
        {
            _importerUnitOfWork = importerUnitOfWork;
            _mapper = mapper;
        }

        public void Create(ExcelData excelData)
        {
            if (excelData == null)
                throw new InvalidOperationException("Excel Data is not provided");
            _importerUnitOfWork.ExcelDatas.Add(
                _mapper.Map<Entities.ExcelData>(excelData));
            _importerUnitOfWork.Save();
        }

        public IList<ExcelData> GetData(int id)
        {
            throw new NotImplementedException();
        }
    }
}
