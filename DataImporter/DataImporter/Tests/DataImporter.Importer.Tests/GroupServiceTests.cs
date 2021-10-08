using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using EO= DataImporter.Importer.Entities;
using DataImporter.Importer.Repositories;
using DataImporter.Importer.Services;
using DataImporter.Importer.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace DataImporter.Importer.Tests
{
    public class GroupServiceTests
    {
        private AutoMock _mock;
        private Mock<IImporterUnitOfWork> _importerUnitOfWork;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IGroupService _groupService;
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
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _groupService = _mock.Create<GroupService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _importerUnitOfWork.Reset();
            _groupRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [Test]
        public void Create_GroupDoesnotExists_ThrowException()
        {

            //Arrange
            Group group = null;

            EO.Group groupEntity = new EO.Group();
            _mapperMock.Setup(x => x.Map<EO.Group>(group)).Returns(groupEntity);

            _groupRepositoryMock.Setup(x => x.Add(groupEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object);

            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act & Assert
            Should.Throw<InvalidOperationException>(
                () => _groupService.Create(group));
        }
        [Test]
        public void Create_GroupExists_AddInDatabase()
        {

            //Arrange
            Group group = new Group { Id = 1,GroupName = "Friends",
                UserId = Guid.Parse("97232217-6677-4d12-8298-08d97ed1842f") };

            EO.Group groupEntity = new EO.Group();
            _mapperMock.Setup(x => x.Map<EO.Group>(group)).Returns(groupEntity);

            _groupRepositoryMock.Setup(x => x.Add(groupEntity)).Verifiable();
            _importerUnitOfWork.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object);

            _importerUnitOfWork.Setup(x => x.Save()).Verifiable();

            //Act
            _groupService.Create(group);

            //Assert
            this.ShouldSatisfyAllConditions(
                () => _groupRepositoryMock.VerifyAll(),
                () => _importerUnitOfWork.VerifyAll());
        }
        [Test]
        public void GetGroups_In_GroupService()
        {
            //Arrange
            Guid id = Guid.Parse("97232217-6677-4d12-8298-08d97ed1842f");
            var groupEntity = new List<EO.Group>()
            {
                new EO.Group()
                { 
                   GroupName = "Friends",
                    UserId = Guid.Parse("97232217-6677-4d12-8298-08d97ed1842f")
                }
            };
            var group = new Group();
            _groupRepositoryMock.Setup(x => x.GetAll()).Returns(groupEntity).Verifiable();
            _importerUnitOfWork.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object).Verifiable();
         
            //Act
            var groupList = _groupService.GetGroups(id);


            //Assert
            this.ShouldSatisfyAllConditions(
                () => groupEntity.ShouldNotBeNull(),
                () => groupList.ShouldNotBeNull());
          

        }

    }
}