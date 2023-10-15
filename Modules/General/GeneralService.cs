using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Context;
using MyAccounts.Database.Models;
using MyAccounts.Dtos;

namespace MyAccounts.Modules.General
{
    public interface IGeneralService
    {
        public Task<InitialDataDto> GetInitialData(int userId);
    }

    public class GeneralService : IGeneralService
    {
        private readonly MyAccountsContext _context;
        private readonly IMapper _mapper;

        public GeneralService(MyAccountsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InitialDataDto> GetInitialData(int userId)
        {
            var persons = await GetPersonsUserCanSee(userId);
            var mainPerson = persons.FirstOrDefault(p => p.UserId == userId && p.IsUser)
                             ?? throw new Exception("Usuario no tiene datos personales");

            _context.Entry(mainPerson).Collection(p => p.Cards).Load();

            return new InitialDataDto
            {
                LoguedUser = new UserDto() { Id = userId, Name = mainPerson.Name, PersonId = mainPerson.Id },
                Persons = _mapper.Map<IList<PersonDto>>(persons),
                Cards = _mapper.Map<IList<CardDto>>(mainPerson.Cards),
            };
        }

        private async Task<IList<Person>> GetPersonsUserCanSee(int userId)
        {
            return await _context.Persons.Where(p => p.UserId == userId || p.IsShared)
                                         .OrderByDescending(p => p.UserId == userId)
                                         .ToListAsync();
        }
    }
}
