using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.Database;
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
            var user = GetUser(userId) ?? throw new InvalidOperationException($"El usuario {userId} no existe");
            var persons = await GetPersonsCanSeeByUser(userId);
            var cards = persons.SelectMany(p => p.Cards ?? new());

            return new InitialDataDto
            {
                LoguedUser = _dtoService.Map<UserDto>(user),
                Persons = _dtoService.Map<IList<PersonDto>>(persons),
                Cards = _dtoService.Map<IList<CardDto>>(cards),
            };
        }

        private async Task<User?> GetUser(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        private async Task<IList<Person>> GetPersonsCanSeeByUser(int userId)
        {
            return await _context.Persons.Where(p => p.UserId == userId || p.IsShared)
                                         .Include(p => p.Cards)
                                         .OrderByDescending(p => p.UserId == userId)
                                         .ToListAsync();
        }
    }
}
