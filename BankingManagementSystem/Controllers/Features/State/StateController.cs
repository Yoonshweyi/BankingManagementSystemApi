using BankingManagementSystem.Models.Setup.State;
using BankingManagementSystem.Services.Features.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.State
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : BaseController
    {
        private readonly StateService _stateService;

        public StateController(StateService stateService)
        {
            _stateService = stateService;
        }

        #region GetStates
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                var model = await _stateService.GetStates();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
        #endregion

        #region GetState ByStateCode
       
        [HttpGet("{stateCode}")]
        public async Task<IActionResult> GetStateByCode(string stateCode)
        {
            try
            {
                var model=await _stateService.GetStateByCode(stateCode);
                return Ok(model);

            }
            catch (Exception ex) 
            { 
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Create State
        [HttpPost]
        public async Task<IActionResult> CreateState(StateRequestModel reqmodel)
        {
            try
            {
                var model = await _stateService.CreateState(reqmodel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
        #region UpdateState
        [HttpPut("{stateCode}")]
        public async Task<IActionResult> UpdateState(string stateCode, StateRequestModel reqmodel)
        {
            try
            {
                var model = await _stateService.UpdateState(stateCode, reqmodel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                {
                    return InternalServerError(ex);

                }
            }
            
        }
        #endregion


        #region DeleteState
        [HttpDelete("{stateCode}")]
        public async Task<IActionResult> DeleteState(string stateCode)
        {
            try
            {
                var model = await _stateService.DeleteState(stateCode);
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
