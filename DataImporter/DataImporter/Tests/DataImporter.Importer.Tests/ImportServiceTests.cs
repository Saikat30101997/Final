using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Repositories;
using DataImporter.Importer.Services;
using DataImporter.Importer.UnitOfWorks;
using EO = DataImporter.Importer.Entities;
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
    public class ImportServiceTests
    {
        private AutoMock _mock;
        private Mock<IImporterUnitOfWork> _importerUnitOfWork;
        private Mock<IImportRepository> _importRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IImportService _importService;
        private Mock<IGroupRepository> _groupRepositoryMock;
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
            _importRepositoryMock = _mock.Mock<IImportRepository>();
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _importService = _mock.Create<ImportService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _importerUnitOfWork.Reset();
            _importRepositoryMock.Reset();
            _groupRepositoryMock.Reset();
            _mapperMock.Reset();
        }
        [Test]
        public void Create_ImportDoesnotExists_ImportService()
        {
            //Arrange
            Import import = null;
            EO.Import importEntity = new EO.Import();
            _mapperMock.Setup(x => x.Map<EO.Import>(import)).Returns(importEntity);
            _importRepositoryMock.Setup(x => x.Add(importEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Imports).Returns(_importRepositoryMock.Object);
            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _importService.Create(import));
        }
        [Test]
        public void Create_ImportExists_ImportService()
        {
            //Arrange
            Import import = new Import { Id = 1,GroupName="Friends", ExcelFileName="demosheet" };
            EO.Import importEntity = new EO.Import();
            _mapperMock.Setup(x => x.Map<EO.Import>(import)).Returns(importEntity);
            _importRepositoryMock.Setup(x => x.Add(importEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Imports).Returns(_importRepositoryMock.Object);
            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _importService.Create(import);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _importRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll());
        }
      
    }
}
