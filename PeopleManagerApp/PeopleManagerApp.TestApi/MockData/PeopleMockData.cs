using PeopleManagerApp.Domain.Dtos;
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
                    Email = "test1@yahoo.com"                    
                },
                new PersonDto
                {
                    Id = 2,
                    FullName = "test 2",
                    Age = 32,
                    Address = "TestAddress2",
                    Email = "test2@yahoo.com"                    
                },
                new PersonDto
                {
                    Id = 3,
                    FullName = "test 3",
                    Age = 22,
                    Address = "TestAddress3",
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
                Email = "test4@yahoo.com"
            };
        }

        public static PersonDto GetNullResponse()
        {
            return null;
        }
    }
}
