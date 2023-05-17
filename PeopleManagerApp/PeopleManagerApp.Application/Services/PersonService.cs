using AutoMapper;
using PeopleManagerApp.Application.Interfaces;
using PeopleManagerApp.Domain.Dtos;
using PeopleManagerApp.Infrastructure.Interfaces;

namespace PeopleManagerApp.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            this._personRepository = personRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> GetAllPeople()
        {            
            var result = await this._personRepository.GetAllPeople();
            var response = this._mapper.Map<IEnumerable<PersonDto>>(result);
            return response;
        }

        public async Task<PersonDto> GetPersonById(long personId)
        {
            var result = await this._personRepository.GetPersonById(personId);
            var response = this._mapper.Map<PersonDto>(result);
            return response;
        }

        public async Task<PersonDto> GetRandomPerson()
        {
            var result = await this._personRepository.GetRandomPerson();
            var response = this._mapper.Map<PersonDto>(result);
            return response;
        }

        public async Task<bool> SoftDeletePerson(long personId)
        {
            return await this._personRepository.SoftDeletePerson(personId);
        }
    }
}
