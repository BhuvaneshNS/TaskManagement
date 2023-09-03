using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.Controllers;
using TaskManagement.Models;

namespace TaskManagement.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IRepository> _mockRepository;
        private Fixture _fixture;
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<IRepository>();
            _homeController = new HomeController(_mockRepository.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                        .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

        }

        [TestMethod]
        public void TestIndex_ReturnedView()
        {
            //Act
            var result = _homeController.Index() as ViewResult;
            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestTaskAssigningGet_ReturnedView()
        {
            //Arrange
            var projects = _fixture.CreateMany<Project>(5).ToList();
            _mockRepository.Setup(repo => repo.GetProjects()).Returns(projects);
            //Act
            var result = _homeController.TaskAssigning() as ViewResult;
            //Assert
            Assert.AreEqual("TaskAssigning", result.ViewName);
        }

        [TestMethod]
        public void TestTaskAssigningGet_ThrowsError()
        {
            //Arrange
            var projects = _fixture.CreateMany<Project>(5).ToList();
            _mockRepository.Setup(repo => repo.GetProjects()).Throws(new Exception());
            //Act
            var result = _homeController.TaskAssigning() as ViewResult;
            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void TestTaskAssigningPost_RedirectOnSuccess()
        {
            //Arrange
            var task = _fixture.Create<Task>();
            _mockRepository.Setup(repo => repo.AssignTask(task));
            //Act
            var result = _homeController.TaskAssigning(task) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestTaskAssigningPost_ModelError()
        {
            //Arrange
            var task = _fixture.Create<Task>();
            _mockRepository.Setup(repo => repo.AssignTask(task));
            _homeController.ModelState.AddModelError("", "Mock error message");
            //Act
            var result = _homeController.TaskAssigning(task) as ViewResult;
            //Assert
            Assert.AreEqual("TaskAssigning", result.ViewName);
        }

        [TestMethod]
        public void TestTaskAssigningPost_ThrowsError()
        {
            //Arrange
            var task = _fixture.Create<Task>();
            _mockRepository.Setup(repo => repo.AssignTask(task)).Throws(new Exception());
            //Act
            var result = _homeController.TaskAssigning(task) as ViewResult;
            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void TestTaskViewGet_ReturnedView()
        {
            //Arrange
            var projects = _fixture.CreateMany<Project>(5).ToList();
            _mockRepository.Setup(repo => repo.GetProjects()).Returns(projects);
            //Act
            var result = _homeController.TaskView() as ViewResult;
            //Assert
            Assert.AreEqual("TaskView", result.ViewName);
        }

        [TestMethod]
        public void TestTaskViewGet_ThrowsError()
        {
            //Arrange
            var projects = _fixture.CreateMany<Project>(5).ToList();
            _mockRepository.Setup(repo => repo.GetProjects()).Throws(new Exception());
            //Act
            var result = _homeController.TaskView() as ViewResult;
            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }
    }
}
