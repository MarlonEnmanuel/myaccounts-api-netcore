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

        public GeneralService(MyAccountsContext context)
        {
            _context = context;
        }

        public async Task<InitialDataDto> GetInitialData(int userId)
        {
            var user = await _context.Users
                                        .Where(u => u.Id == userId)
                                        .Include(u => u.Persons)
                                        .ThenInclude(p => p.Cards)
                                        .FirstOrDefaultAsync()
                                    ?? throw new Exception("Usuario no encontrado");
            return new InitialDataDto
            {
                Persons = user.Persons ?? Array.Empty<Person>(),
                Cards = user.UserPerson?.Cards ?? Array.Empty<Card>(),
            };
        }
    }
}
