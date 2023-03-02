using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAccounts.Dtos;
using MyAccounts.Modules.General;

namespace MyAccounts.Controllers
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
