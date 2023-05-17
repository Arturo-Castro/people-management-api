using PeopleManagerApp.Domain.Dtos;
using PeopleManagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagerApp.TestApi.MockData
{
    public class PeopleMockData
    {
        public static List<PersonDto> GetPeople()
        {
            return new List<PersonDto>
            {
                new PersonDto
                {
                    Id = 1,
                    FullName = "test 1",
                    Age = 22,
                    Address = "TestAddress1",
                    Phone = "123",
                    Email = "test1@yahoo.com"
                },
                new PersonDto
                {
                    Id = 2,
                    FullName = "test 2",
                    Age = 32,
                    Address = "TestAddress2",
                    Phone = "1234",
                    Email = "test2@yahoo.com"                    
                },
                new PersonDto
                {
                    Id = 3,
                    FullName = "test 3",
                    Age = 22,
                    Address = "TestAddress3",
                    Phone = "12345",
                    Email = "test3@yahoo.com"                    
                }
            };
        }

        public static List<PersonDto> EmptyPersonDtoList()
        {
            return new List<PersonDto>();
        }

        public static PersonDto GetPersonDto(long id)
        {
            return new PersonDto 
            {
                Id = id,
                FullName = "test 4",
                Age = 15,
                Address = "TestAddress4",
                Phone = "123",
                Email = "test4@yahoo.com"
            };
        }

        public static PersonDto GetNullResponse()
        {
            return null;
        }

        public static IEnumerable<Person> EmptyPersonEntityEnumerable()
        {
            return Enumerable.Empty<Person>();
        }

        public static IEnumerable<Person> GetPeopleEntityList()
        {
            IEnumerable<Person> personEntityList = new List<Person>()
            { 
                new Person
                {
                    Id = 1,
                    Name = "test",
                    LastName = "1",
                    Age = 22,
                    Address = "TestAddress1",
                    Phone = "123",
                    Email = "test1@yahoo.com",
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Person
                {
                    Id = 2,
                    Name = "test",
                    LastName = "2",
                    Age = 32,
                    Address = "TestAddress2",
                    Phone = "1234",
                    Email = "test2@yahoo.com",
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Person
                {
                    Id = 3,
                    Name = "test",
                    LastName = "3",
                    Age = 22,
                    Address = "TestAddress3",
                    Phone = "12345",
                    Email = "test3@yahoo.com",
                    IsDeleted = false,
                    DeletedAt = null
                }
            };
            return personEntityList;            
        }

        public static Person GetPersonEntity(long id)
        {
            return new Person
            {
                Id = id,
                Name = "test",
                LastName = "4",
                Age = 15,
                Address = "TestAddress4",
                Phone = "123",
                Email = "test4@yahoo.com",
                IsDeleted = false,
                DeletedAt = null
            };
        }

        public static Person GetEntityNullResponse()
        {
            return null;
        }
    }
}
