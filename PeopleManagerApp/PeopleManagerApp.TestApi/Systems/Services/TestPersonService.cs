using AutoMapper;
using FluentAssertions;
using Moq;
using PeopleManagerApp.Application.Automapper;
using PeopleManagerApp.Application.Services;
using PeopleManagerApp.Domain.Dtos;
using PeopleManagerApp.Infrastructure.Interfaces;
using PeopleManagerApp.TestApi.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PeopleManagerApp.TestApi.Systems.Services
{
    public class TestPersonService
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly PersonService _sut;
        private readonly IMapper _mapper;

        public TestPersonService()
        {
            this._personRepositoryMock = new Mock<IPersonRepository>();
            var myProfile = new MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            this._mapper = mapper;
            this._sut = new PersonService(this._personRepositoryMock.Object, this._mapper);
        }

        [Fact]
        public async Task GetAllPeople_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
        {
            //Arrange
            this._personRepositoryMock.Setup(_ => _.GetAllPeople()).ReturnsAsync(PeopleMockData.EmptyPersonEntityEnumerable);

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GetAllPeople_ReturnsPersonDtoList_WhenRepositoryReturnsPersonEntityList()
        {
            //Arrange
            this._personRepositoryMock.Setup(_ => _.GetAllPeople()).ReturnsAsync(PeopleMockData.GetPeopleEntityList());

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().BeEquivalentTo(PeopleMockData.GetPeople());
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnPersonDto_WhenRepositoryReturnsPersonEntity()
        {
            //Arrange
            var expectedPersonId = 2;
            this._personRepositoryMock.Setup(_ => _.GetPersonById(expectedPersonId)).ReturnsAsync(PeopleMockData.GetPersonEntity(expectedPersonId));

            //Act
            var result = await this._sut.GetPersonById(expectedPersonId);

            //Assert
            result.Should().BeEquivalentTo(PeopleMockData.GetPersonDto(expectedPersonId));
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //Arrange
            var expectedPersonId = 2;
            this._personRepositoryMock.Setup(_ => _.GetPersonById(expectedPersonId)).ReturnsAsync(PeopleMockData.GetEntityNullResponse());

            //Act
            var result = await this._sut.GetPersonById(expectedPersonId);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturnPersonDto_WhenRepositoryReturnsPersonEntity()
        {
            //Arrange
            var random = new Random();
            var randomSkip = random.Next(0, 3);
            this._personRepositoryMock.Setup(_ => _.GetRandomPerson()).ReturnsAsync(PeopleMockData.GetPeopleEntityList().Skip(randomSkip).First());

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.Should().BeOfType<PersonDto>();
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            //Arrange
            this._personRepositoryMock.Setup(_ => _.GetRandomPerson()).ReturnsAsync(PeopleMockData.GetEntityNullResponse());

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturnTrue_WhenRepositoryLogicallyDeletesPersonEntity()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            this._personRepositoryMock.Setup(_ => _.SoftDeletePerson(randomNumber)).ReturnsAsync(true);

            //Act
            var result = await this._sut.SoftDeletePerson(randomNumber);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturnFalse_WhenRepositoryReturnsFalse()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            this._personRepositoryMock.Setup(_ => _.SoftDeletePerson(randomNumber)).ReturnsAsync(false);

            //Act
            var result = await this._sut.SoftDeletePerson(randomNumber);

            //Assert
            result.Should().BeFalse();
        }
    }
}
