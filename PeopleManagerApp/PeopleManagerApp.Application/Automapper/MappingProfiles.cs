using AutoMapper;
using PeopleManagerApp.Domain.Dtos;
using PeopleManagerApp.Domain.Entities;


namespace PeopleManagerApp.Application.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName));
        }
    }
}
