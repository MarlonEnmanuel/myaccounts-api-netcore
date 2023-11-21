using AutoMapper;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.General.Dtos;

namespace MyAccounts.Api.Modules.General
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<User, UserAuthDto>();
            CreateMap<Person, PersonAuthDto>();
            CreateMap<Card, CardAuthDto>();
        }
    }
}
