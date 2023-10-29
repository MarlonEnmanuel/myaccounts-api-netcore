using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAccounts.Api.Dtos;
using MyAccounts.Api.Modules.General;

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
        public async Task<InitialDataDto> GetInitialData ()
        {
            return await _generalService.GetInitialData(UserId);
        }

    }
}
