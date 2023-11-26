using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.Database;
using MyAccounts.Api.Database.Models;
using MyAccounts.Api.Modules.General.Dtos;
using MyAccounts.Api.Modules.Shared;

namespace MyAccounts.Api.Modules.General
{
    public class GeneralService : IGeneralService
    {
        private readonly MyAccountsContext _context;
        private readonly IDtoService _dtoService;

        public GeneralService(MyAccountsContext context, IDtoService dtoService)
        {
            _context = context;
            _dtoService = dtoService;
        }

        public async Task<AuthDataDto> GetAuthData(int userId)
        {
            var user = await GetUser(userId) ?? throw new InvalidOperationException($"El usuario {userId} no existe");
            var persons = await GetPersonsCanSeeByUser(userId);
            var cards = persons.Where(p => p.UserId == userId).SelectMany(p => p.Cards ?? new());

            return new AuthDataDto
            {
                User = _dtoService.Map<UserAuthDto>(user),
                Persons = _dtoService.Map<List<PersonAuthDto>>(persons),
                Cards = _dtoService.Map<List<CardAuthDto>>(cards),
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
