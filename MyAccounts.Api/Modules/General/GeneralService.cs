using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.Database.Context;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Dtos;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Api.Modules.General
{
    public interface IGeneralService
    {
        public Task<InitialDataDto> GetInitialData(int userId);
    }

    public class GeneralService : IGeneralService
    {
        private readonly MyAccountsContext _context;
        private readonly IDtoService _dtoService;

        public GeneralService(MyAccountsContext context, IDtoService dtoService)
        {
            _context = context;
            _dtoService = dtoService;
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
                Persons = _dtoService.Map<IList<PersonDto>>(persons),
                Cards = _dtoService.Map<IList<CardDto>>(mainPerson.Cards),
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
