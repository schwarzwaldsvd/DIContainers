using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonRepository.Service.MyService;
using System.Collections.Generic;
using PeopleViewer.SharedObjects;
using Moq;
using System.Linq;

namespace PersonRepository.Service.Test
{
    [TestClass]
    public class ServiceRepositoryTest
    {
        IPersonService _service;

        [TestInitialize]
        public void Setup()
        {
            var people = new List<Person>()
            {
                new Person() {FirstName = "John", LastName = "Smith",
                              Rating = 7, StartDate = DateTime.Parse("10/01/2000")},
                new Person() {FirstName = "Mary", LastName = "Thomas",
                              Rating = 9, StartDate = DateTime.Parse("07/23/1971")},
            };

            var svcMock = new Mock<IPersonService>();
            svcMock.Setup(s => s.GetPeople()).Returns(people);
            svcMock.Setup(s => s.GetPerson(It.IsAny<string>()))
                .Returns((string n) => people.FirstOrDefault(p => p.LastName == n));
            _service = svcMock.Object;
        }

        [TestMethod]
        public void GetPeople_OnExecute_ReturnsPeople()
        {
            // Arrange
            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;

            // Act
            var output = repo.GetPeople();

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual(2, output.Count());
        }

        [TestMethod]
        public void GetPerson_OnExecuteWithValidValue_ReturnsPerson()
        {
            // Arrange
            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;

            // Act
            var output = repo.GetPerson("Smith");

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual("Smith", output.LastName);
        }

        [TestMethod]
        public void GetPerson_OnExecuteWithInvalidValue_ReturnsPerson()
        {
            // Arrange
            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;

            // Act
            var output = repo.GetPerson("NOTAREALPERSON");

            // Assert
            Assert.IsNull(output);
        }
    }
}
