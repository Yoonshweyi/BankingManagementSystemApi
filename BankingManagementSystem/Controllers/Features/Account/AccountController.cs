using BankingManagementSystem.Models.Setup.Account;
using BankingManagementSystem.Services.Features.Account;
//using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        #region get Account lists
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var model = await _accountService.GetAccounts();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
        #region Get account list by Pagination

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetAccountList(int pageNo, int pageSize)
        {
            try
            {
                var model = await _accountService.GetAccountList(pageNo, pageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region get Account By AccountNo

        [HttpGet("{accountNo}")]
        public async Task<IActionResult> GetAccount(string accountNo)
        {
            try
            {
                var model = await _accountService.GetAccont(accountNo);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Account Create
        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountRequestModel requestModel)
        {
            try
            {
                var model = await _accountService.CreateAccount(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Account update
       
        [HttpPut("{accountNo}")]
        public async Task<IActionResult> UpdateAccount(string accountNo, AccountRequestModel requestModel)
        {
            try
            {
                var model = await _accountService.UpdateAccount(accountNo, requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
        #endregion

        #region Account Delete

        [HttpDelete("{accountNo}")]
        public async Task<IActionResult> DeleteAccount(string accountNo)
        {
            try
            {
                var model = await _accountService.DeleteAccount(accountNo);
                return Ok(model);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion


    }
}
