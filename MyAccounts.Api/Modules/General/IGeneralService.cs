using MyAccounts.Api.Modules.General.Dtos;

namespace MyAccounts.Api.Modules.General
{
    public interface IGeneralService
    {
        public Task<AuthDataDto> GetAuthData(int userId);
    }
}
