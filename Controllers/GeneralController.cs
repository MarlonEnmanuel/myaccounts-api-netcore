using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetInitialData ()
        {
            var data = await _generalService.GetInitialData(UserId);
            return Ok(data);
        }

    }
}
