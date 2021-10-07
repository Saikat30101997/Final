using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Repositories;
using EO = DataImporter.Importer.Entities;
using DataImporter.Importer.Services;
using DataImporter.Importer.UnitOfWorks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace DataImporter.Importer.Tests
{
    public class ExcelDataServiceTests
    {
        private AutoMock _mock;
        private Mock<IImporterUnitOfWork> _importerUnitOfWork;
        private Mock<IExcelDataRepository> _excelDataRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IExcelDataService _excelDataService;
      
        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _importerUnitOfWork = _mock.Mock<IImporterUnitOfWork>();
            _excelDataRepositoryMock = _mock.Mock<IExcelDataRepository>();
            _excelDataRepositoryMock = _mock.Mock<IExcelDataRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _excelDataService = _mock.Create<ExcelDataService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _importerUnitOfWork.Reset();
            _excelDataRepositoryMock.Reset();
            _excelDataRepositoryMock.Reset();
            _mapperMock.Reset();
        }
        [Test]
        public void Create_ExcelDataDoesnotExists_InExcelDataService()
        {
            //Arrange
            ExcelData excelData = null;

            EO.ExcelData excelDataEntity= new EO.ExcelData();

            _mapperMock.Setup(x => x.Map<EO.ExcelData>(excelData)).Returns(excelDataEntity).Verifiable();
            _excelDataRepositoryMock.Setup(x => x.Add(excelDataEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.ExcelDatas).Returns(_excelDataRepositoryMock.Object);
            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();


            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _excelDataService.Create(excelData));
            
        }
        [Test]
        public void Create_ExcelDataExists_InExcelDataService()
        {
            //Arrange
            ExcelData excelData = new ExcelData
            {
                Id=1,
                GroupId=2,
                ImportId=4,
                GroupName="Friends"
            };

            EO.ExcelData excelDataEntity = new EO.ExcelData();

            _mapperMock.Setup(x => x.Map<EO.ExcelData>(excelData)).Returns(excelDataEntity).Verifiable();
            _excelDataRepositoryMock.Setup(x => x.Add(excelDataEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.ExcelDatas).Returns(_excelDataRepositoryMock.Object);
            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _excelDataService.Create(excelData);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _excelDataRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll());
        }
        [Test]
        public void GetAllData_In_ExcelDataService()
        {
            //Arrange
            Guid Id = Guid.Parse("97232217-6677-4d12-8298-08d97ed1842f");
            string groupName = "Friends";
            DateTime dateFrom = DateTime.Now;
            DateTime dateTo = DateTime.Parse("30/10/2021");
            IList<EO.ExcelData> exceldataEntity = new List<EO.ExcelData>() 
            { 
                new EO.ExcelData()
                {
                    UserId=Id,
                    GroupName = groupName,
                    ImportDate = DateTime.Now
                }
            };
            _excelDataRepositoryMock.Setup(x => x.GetAll()).Returns(exceldataEntity).Verifiable();
            _importerUnitOfWork.Setup(x => x.ExcelDatas).Returns(_excelDataRepositoryMock.Object);

           //Act
           var excelData= _excelDataService.GetAllData(Id, groupName, dateFrom, dateTo);

            //Assert

            this.ShouldSatisfyAllConditions(
                () => _excelDataRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll(),
                () => excelData.ShouldNotBeNull());

        }
    }
}
