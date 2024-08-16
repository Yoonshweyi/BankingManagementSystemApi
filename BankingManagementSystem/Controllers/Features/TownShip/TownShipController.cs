using BankingManagementSystem.Models.Setup.Township;
using BankingManagementSystem.Services.Features.State;
using BankingManagementSystem.Services.Features.TownShip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.TownShip
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownShipController : BaseController
    {
        private readonly TownShipService _townShipService;

        public TownShipController(TownShipService townShipService)
        {
            _townShipService = townShipService;
        }

        #region GetStates
        [HttpGet]
        public async Task<IActionResult> GetTownShips()
        {
            try
            {
                var model =await _townShipService.GetTownshipList();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region Get TownShip ByTownShipCode
        [HttpGet("{townshipCode}")]
        public async Task<IActionResult> GetTownShipByCode(string townshipCode)
        {
            try
            {
                var model = await _townShipService.GetTownShipByCode(townshipCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
       
        #region Create Township
        [HttpPost]
        public async Task<IActionResult> CreateTownShip([FromBody] TownshipRequestModel reqModel)
        {
            try
            {
                var model=await _townShipService.CreateTownShip(reqModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Update Township
        [HttpPut("{townshipCode}")]
        public async Task<IActionResult> UpdateTownship(string townshipCode, [FromBody] TownshipRequestModel reqModel)
        {
            try
            {
                var model = await _townShipService.UpdateTownship(townshipCode, reqModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
          
        }
        #endregion

        #region Delete TownShip
        [HttpDelete("{townShipCode}")]
        public async Task<IActionResult> DeleteTownShip(string townShipCode)
        {
            try
            {
                var model = await _townShipService.DeleteTownShip(townShipCode);
                return Ok(model);
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region get Township ByStateCode
        [HttpGet("StateCode/{stateCode}")]
        public async Task<IActionResult> GetTownshipByStateCode(string stateCode)
        {
            try
            {
                var model = await _townShipService.GtTownshipByStateCode(stateCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

    }
}
