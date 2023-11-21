using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAccounts.Api.Modules.General;
using MyAccounts.Api.Modules.General.Dtos;

namespace MyAccounts.Api.Controllers
{
    [Route("api/general")]
    [ApiController]
    public class GeneralController : BaseController
    {
        public IGeneralService _generalService { get; set; }

        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        [HttpGet("initial")]
        [Authorize]
        public async Task<AuthDataDto> GetAuthData ()
        {
            return await _generalService.GetAuthData(UserId);
        }

    }
}
