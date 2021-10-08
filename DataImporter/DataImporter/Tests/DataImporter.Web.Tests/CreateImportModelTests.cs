using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using DataImporter.Web.Models;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Web.Tests
{
    [ExcludeFromCodeCoverage]
    public class CreateImportModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupService> _groupServiceMock;
        private CreateImportModel _model;

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
            _groupServiceMock = _mock.Mock<IGroupService>();
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<CreateImportModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupServiceMock?.Reset();
            _mapperMock?.Reset();
        }

        [Test]
        public void Create_Group_InCreateImportModel()
        {
            //Arrange
            Guid Id = Guid.Parse("97232217-6677-4d12-8298-08d97ed1842f");
            string Name = "Friends";
            var group = new Group
            {
                GroupName = Name,
                UserId = Id
            };

            _groupServiceMock.Setup(x => x.Create(group)).Verifiable();

            //Act
            _model.Create(Id);

            //Assert
            _model.ShouldSatisfyAllConditions();
        }
    }
}