﻿using PeopleManagerApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagerApp.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllPeople();
        Task<PersonDto> GetPersonById(long personId);
        Task<PersonDto> GetRandomPerson();
        Task<bool> SoftDeletePerson(long personId);
    }
}
