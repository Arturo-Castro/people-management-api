using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PeopleManagerApp.Api.Controllers;
using PeopleManagerApp.Application.Interfaces;
using PeopleManagerApp.TestApi.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PeopleManagerApp.TestApi.Systems.Controllers
{    
    public class TestPeopleController
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly PeopleController _sut;        

        public TestPeopleController()
        {
            this._personServiceMock = new Mock<IPersonService>();
            this._sut = new PeopleController(_personServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPeople_ShouldReturn200Status()
        {
            //Arrange            
            this._personServiceMock.Setup(_ => _.GetAllPeople()).ReturnsAsync(PeopleMockData.GetPeople());           

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task GetAllPeople_ShouldReturn404Status()
        {
            //Arrange            
            this._personServiceMock.Setup(_ => _.GetAllPeople()).ReturnsAsync(PeopleMockData.EmptyPersonDtoList());

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task GetPersonById_ShouldReturn200Status()
        {
            //Arrange
            var expectedPersonId = 3;
            this._personServiceMock.Setup(_ => _.GetPersonById(expectedPersonId)).ReturnsAsync(PeopleMockData.GetPersonDto(expectedPersonId));

            //Act
            var result = await this._sut.GetPersonById(expectedPersonId);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task GetPersonById_ShouldReturn404Status()
        {
            //Arrange
            var expectedPersonId = 6;
            this._personServiceMock.Setup(_ => _.GetPersonById(expectedPersonId)).ReturnsAsync(PeopleMockData.GetNullResponse());

            //Act
            var result = await this._sut.GetPersonById(expectedPersonId);

            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturn200Status()
        {
            //Arrange
            var random = new Random();
            var randomSkip = random.Next(0, 3);
            this._personServiceMock.Setup(_ => _.GetRandomPerson()).ReturnsAsync(PeopleMockData.GetPeople().Skip(randomSkip).First());

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturn404Status()
        {
            //Arrange            
            this._personServiceMock.Setup(_ => _.GetRandomPerson()).ReturnsAsync(PeopleMockData.GetNullResponse());

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturn200Status()
        {
            //Arrange
            var expectedPersonId = 6;
            this._personServiceMock.Setup(_ => _.SoftDeletePerson(expectedPersonId)).ReturnsAsync(true);

            //Act
            var result = await this._sut.SoftDeletePerson(expectedPersonId);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturn404Status()
        {
            //Arrange
            var expectedPersonId = 6;
            this._personServiceMock.Setup(_ => _.SoftDeletePerson(expectedPersonId)).ReturnsAsync(false);

            //Act
            var result = await this._sut.SoftDeletePerson(expectedPersonId);

            //Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
        }
    }
}
