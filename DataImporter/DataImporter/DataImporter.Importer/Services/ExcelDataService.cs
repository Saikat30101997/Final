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

        public List<ExcelData> GetAllData(Guid id, string groupName, DateTime dateFrom, DateTime dateTo)
        {
            var data = new List<ExcelData>();
            var excelDataEntity = _importerUnitOfWork.ExcelDatas.GetAll();
            if (dateFrom > dateTo) dateTo = dateFrom;
            var filterData = (from exceldata in excelDataEntity
                              where exceldata.GroupName == groupName && exceldata.UserId == id 
                              select exceldata).ToList();

            if (filterData.Any(x => x.ImportDate == dateFrom) == false && filterData.Any(x=>x.ImportDate==dateTo)==true) 
                dateFrom = filterData.Min(x => x.ImportDate);

            if (filterData.Any(x => x.ImportDate == dateFrom) == true && filterData.Any(x => x.ImportDate == dateTo) == false)
                dateTo = dateFrom;

            if (filterData.Any(x => x.ImportDate > dateFrom) == true && filterData.Any(x => x.ImportDate == dateFrom) == false)
            {
                dateFrom = (filterData.TakeWhile(x => x.ImportDate > dateFrom)).ToList()[0].ImportDate;

            }

            var filterbydate = (from exceldata in filterData
                                where exceldata.ImportDate >= dateFrom
                                && exceldata.ImportDate <= dateTo
                                select exceldata).ToList();
            foreach (var item in filterbydate)
                {
                    var ex = _mapper.Map<ExcelData>(item);
                    data.Add(ex);
                }
            
            return data;
        }

        public IList<ExcelData> GetData(int id)
        {
            var exceldatas = _importerUnitOfWork.ExcelDatas.Get(x => x.ImportId == id,string.Empty);
            var data = new List<ExcelData>();
            foreach (var exceldata in exceldatas)
            {
                var excel = _mapper.Map<ExcelData>(exceldata);
                data.Add(excel);
            }
            return data;
        }
    }
}
