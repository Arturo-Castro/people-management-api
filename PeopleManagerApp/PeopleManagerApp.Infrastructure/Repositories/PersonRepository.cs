using Microsoft.EntityFrameworkCore;
using PeopleManagerApp.Domain.Entities;
using PeopleManagerApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagerApp.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _context;
        public PersonRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            var result = await this._context.People
                .OrderBy(p => p.Name)
                .ToListAsync();
            return result;
        }

        public async Task<Person> GetPersonById(long personId)
        {
            var result = await this._context.People
                .FirstOrDefaultAsync(p => p.Id == personId);
            return result;
        }
    }
}
