using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importer.BusinessObjects;
using DataImporter.Importer.Services;
using DataImporter.Web.Models;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Web.Tests
{
    public class EditGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupService> _groupServiceMock;
        private Mock<IUpdateService> _updateServiceMock;
        private EditGroupModel _model;

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
            _updateServiceMock = _mock.Mock<IUpdateService>();
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<EditGroupModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupServiceMock?.Reset();
            _updateServiceMock?.Reset();
            _mapperMock?.Reset();
        }
        [Test]
        public void LoadModelData_In_EditGroupModel()
        {

            //Arrange
            const int id = 1;
            var group = new Group
            {
                GroupName = "Friends",
                Id = 1
            };

            _updateServiceMock.Setup(x => x.GetGroup(id)).Returns(group).Verifiable();

            //Act 
            _model.LoadModelData(id);

            //Assert
            _updateServiceMock.VerifyAll();
        }
        [Test]
        public void UpdateGroup_In_EditGroupModel()
        {
            //Arrange
            _model.Id = 1;
            _model.Name = "Friends";
            var group = new Group
            {
                Id = _model.Id,
                GroupName = _model.Name
            };

            _updateServiceMock.Setup(x => x.UpdateGroupName(group)).Verifiable();
            
            //Act
            _model.Update();


            //Assert
            _model.ShouldSatisfyAllConditions();
        }
    }
}
