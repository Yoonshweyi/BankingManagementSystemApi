using BankingManagementSystem.Models.Setup.Account;
using BankingManagementSystem.Models.Setup.TransactionHistory;
using BankingManagementSystem.Models.Setup.Transfer;
using BankingManagementSystem.Services.Features.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        #region Get History
        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetTransactionHistory(int pageNo, int pageSize)
        {
            try
            {
                var model = await _transactionService.TransactionHistory(pageNo, pageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion


        #region Get TransactionHistory By Date
        [HttpPost("TransactionHistory")]
        public async Task<IActionResult> TransactionHistory(TransactionHistorySearchModel reqModel)
        {
            try
            {
                var model = await _transactionService.TransactionHistoryWithDate(reqModel.FromDate, reqModel.PageNo, reqModel.PageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Deposit
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(TransactionrequestModel reqModel)
        {
            try
            {
                var model = await _transactionService.Deposit(reqModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Withdrawl
        [HttpPost("Withdrawl")]
        public async Task<IActionResult> Withdrawl(TransactionrequestModel requestModel)
        {
            try
            {
                var model = await _transactionService.Withdraw(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        #endregion

        #region Transfer
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(TransferModel reqModel)
        {
            try
            {
                var model = await _transactionService.Transfer(reqModel);
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
