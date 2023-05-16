using PeopleManagerApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagerApp.Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPeople();
    }
}
