using BankingManagementSystem.Services.Features.Generate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.Generate
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : BaseController
    {
        private readonly GenerateService _generateService;

        public GenerateController(GenerateService generateService)
        {
            _generateService = generateService;
        }
        //#region Generate accounts

        //[HttpPost("Generate")]
        //public async Task<IActionResult> GenerateAccounts(int count, int year)
        //{
        //    var model = await _transactionService.GenerateAccounts(count, year);
        //    return Ok(model);
        //}

        //#endregion
        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateAccounts(int count, int year)
        {
            var model=await _generateService.GenerateAccounts(count, year);
            return Ok(model);
        }
    }
}
