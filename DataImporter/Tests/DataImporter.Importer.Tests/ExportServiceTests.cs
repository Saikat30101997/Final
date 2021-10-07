using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.Repositories;
using DataImporter.Importer.Services;
using DataImporter.Importer.UnitOfWorks;
using EO = DataImporter.Importer.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Importer.BusinessObjects;

namespace DataImporter.Importer.Tests
{
    public class ExportServiceTests
    {
        private AutoMock _mock;
        private Mock<IImporterUnitOfWork> _importerUnitOfWork;
        private Mock<IExportRepository> _exportRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IExportService _exportService;
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
            _exportRepositoryMock = _mock.Mock<IExportRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _exportService = _mock.Create<ExportService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _importerUnitOfWork.Reset();
            _exportRepositoryMock.Reset();
            _mapperMock.Reset();
        }
        [Test]
        public void Delete_ByExportId_InExportService()
        {
            //Arrange
            const int id = 1;
            _exportRepositoryMock.Setup(x => x.Remove(id)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Exports).Returns(_exportRepositoryMock.Object);
            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _exportService.Delete(id);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _exportRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll());
        }
        [Test]
        public void Create_ExportDoesnotExists_throwException()
        {
            //Arrange
            Export export = null;
            EO.Export exportEntity = new EO.Export();
            _mapperMock.Setup(x => x.Map<EO.Export>(export)).Returns(exportEntity);

            _exportRepositoryMock.Setup(x => x.Add(exportEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Exports).Returns(_exportRepositoryMock.Object);

            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act && Assert
            Should.Throw<InvalidOperationException>(
                () => _exportService.Create(export));
        }
        [Test]
        public void Create_ExportExists_InExportService()
        {
            //Arrange
            var export = new Export
            {
                Id=1,
                ExcelFileName = "demosheet",
                GroupId = 4
            };

            EO.Export exportEntity = new EO.Export();
            _mapperMock.Setup(x => x.Map<EO.Export>(export)).Returns(exportEntity);

            _exportRepositoryMock.Setup(x => x.Add(exportEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Exports).Returns(_exportRepositoryMock.Object);

            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _exportService.Create(export);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _exportRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll());
        }
    }
}
