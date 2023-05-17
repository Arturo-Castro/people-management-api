using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PeopleManagerApp.Infrastructure;
using PeopleManagerApp.Infrastructure.Repositories;
using PeopleManagerApp.TestApi.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PeopleManagerApp.TestApi.Systems.Repositories
{
    public class TestPersonRepository : IDisposable
    {
        private readonly ApplicationContext _dbContext;
        private readonly PersonRepository _sut;
        public TestPersonRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            this._dbContext = new ApplicationContext(options);
            this._dbContext.Database.EnsureCreated();
            this._sut = new PersonRepository(this._dbContext);

        }

        [Fact]
        public async Task GetAllPeople_ShouldReturnAllPeopleEntityList_WhenIsDeletedIsFalse()
        {
            //Arrange
            await this._dbContext.AddRangeAsync(PeopleMockData.GetPeopleEntityList());
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().HaveCount(PeopleMockData.GetPeopleEntityList().Count());
        }

        [Fact]
        public async Task GetAllPeople_ShouldReturnEmpty_WhenEveryPersonHasIsDeletedSetToTrue()
        {
            //Arrange
            var deletedPeople = PeopleMockData.GetDeletedPeopleEntityList();
            await this._dbContext.AddRangeAsync(deletedPeople);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllPeople_ShouldOnlyReturnPeople_WithIsDeletedSetToFalse()
        {
            //Arrange
            var mixedPeople = PeopleMockData.GetMixedPeopleEntityList();
            await this._dbContext.AddRangeAsync(mixedPeople);
            await this._dbContext.SaveChangesAsync();

            var expectedCount = PeopleMockData.GetMixedPeopleEntityList().Count(p => !p.IsDeleted);

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().NotContain(p => p.IsDeleted);
            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public async Task GetAllPeople_ShouldReturnPeopleOrderedByName()
        {
            //Arrange
            var unorderedPeople = PeopleMockData.GetPeopleEntityListWithUnorderedNames();
            await this._dbContext.AddRangeAsync(unorderedPeople);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetAllPeople();

            //Assert
            result.Should().BeInAscendingOrder(p => p.Name);
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnPerson_WhenPersonHasIsDeletedSetToFalse()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            var personEntity = PeopleMockData.GetPersonEntity(randomNumber);
            await this._dbContext.AddRangeAsync(personEntity);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetPersonById(randomNumber);

            //Assert
            result.Should().BeEquivalentTo(personEntity);
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnNull_WhenPersonHasIsDeletedSetToTrue()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            var deletedPersonEntity = PeopleMockData.GetDeletedPersonEntity(randomNumber);
            await this._dbContext.AddRangeAsync(deletedPersonEntity);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetPersonById(randomNumber);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnNull_WhenPersonDoesNotExist()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();

            //Act
            var result = await this._sut.GetPersonById(randomNumber);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturnPerson_WhenPersonHasIsDeletedSetToFalse()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            var activePerson = PeopleMockData.GetPersonEntity(randomNumber);
            await this._dbContext.AddRangeAsync(activePerson);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.Should().BeEquivalentTo(activePerson);
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturnNull_WhenPersonDoesNotExist()
        {
            //Arrange            

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetRandomPerson_ShouldReturnNull_WhenPersonHasIsDeletedSetToTrue()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next();
            var deletedPersonEntity = PeopleMockData.GetDeletedPersonEntity(randomNumber);
            await this._dbContext.AddRangeAsync(deletedPersonEntity);
            await this._dbContext.SaveChangesAsync();

            //Act
            var result = await this._sut.GetRandomPerson();

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturnTrueAndModifyIsDeletedAndDeletedAt_WhenPersonExistsAndIsDeletedIsFalse()
        {
            // Arrange
            var random = new Random();
            long randomNumber = random.Next();
            var activePerson = PeopleMockData.GetPersonEntity(randomNumber);
            await this._dbContext.AddAsync(activePerson);
            await this._dbContext.SaveChangesAsync();

            // Act
            var result = await this._sut.SoftDeletePerson(randomNumber);

            // Assert
            result.Should().BeTrue();

            var updatedPerson = await this._dbContext.People.FindAsync(randomNumber);
            updatedPerson.IsDeleted.Should().BeTrue();
            updatedPerson.DeletedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(1000));
        }

        [Fact]
        public async Task SoftDeletePerson_ShouldReturnFalse_WhenPersonDoesNotExistOrHasIsDeletedSetToTrue()
        {
            // Arrange
            var random = new Random();
            var randomNumber = random.Next();
            var deletedPersonEntity = PeopleMockData.GetDeletedPersonEntity(randomNumber);
            await this._dbContext.AddAsync(deletedPersonEntity);
            await this._dbContext.SaveChangesAsync();

            var nonExistentPersonId = randomNumber + 1;

            // Act
            var resultForDeletedPerson = await this._sut.SoftDeletePerson(randomNumber);
            var resultForNonExistentPerson = await this._sut.SoftDeletePerson(nonExistentPersonId);

            // Assert
            resultForDeletedPerson.Should().BeFalse();
            resultForNonExistentPerson.Should().BeFalse();
        }

        public void Dispose()
        {
            this._dbContext.Database.EnsureDeleted();
            this._dbContext.Dispose();
        }
    }
}
