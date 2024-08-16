using BankingManagementSystem.Models.Setup;
using BankingManagementSystem.Models.Setup.Account;
using BankingManagementSystem.Models.Setup.TransactionHistory;
using BankingManagementSystem.Models.Setup.Transfer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Services.Features.Transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _appDbContext;

        public TransactionService(AppDbContext appDbContext)
        {

        _appDbContext = appDbContext; 
        }

        #region TransactionHistory
        public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo,int pageSize)
        {
            TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
            var query = _appDbContext.TblTransactionHistories.AsNoTracking();
            var result=await query.OrderByDescending(x=>x.TransactionDate)
                .Skip((pageNo-1)*pageSize)
                .Take(pageSize).ToListAsync();

            var count=await query.CountAsync();
            int pageCount=count/pageSize;
            if (count % pageSize > 0) pageCount++;
            var lst=result.Select(x=>x.Change()).ToList();
            model = new TransactionHistoryListResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;

        }
        #endregion

        #region TransactionHistory With Date
        public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDate(DateTime? date,int pageNo,int pageSize)
        {
            TransactionHistoryListResponseModel model=new TransactionHistoryListResponseModel();
            var query=_appDbContext.TblTransactionHistories.AsNoTracking();

            if (date.HasValue)
            {
                query=query.Where(x=>x.TransactionDate.Date == date.Value.Date);
            }
            var result=await query.OrderByDescending(x => x.TransactionDate)
                .Skip((pageNo-1)*pageSize) .Take(pageSize).ToListAsync();

            var count=await query.CountAsync();
            int pageCount=count/pageSize;
            if(count % pageSize > 0)pageCount++;
            var lst=result.Select(x=>x.Change()).ToList();
            model = new TransactionHistoryListResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")

            };
            return model;
        }
        #endregion


        #region Deposit
        public async Task<AccountResponseModel> Deposit(TransactionrequestModel reqModel)
        {
            var query=_appDbContext.TblAccounts.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.AccountNo == reqModel.AccountNo);
            if(item is null)
            {
                throw new Exception("Invalid Account");
            }
            var transaction= await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                item.Balance += reqModel.Amount;
                _appDbContext.TblAccounts.Update(item);
                int result = await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
            }
            AccountResponseModel model = new AccountResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Deposit Successfully.")
            };
            return model;
        }
        #endregion

        #region Withdrawl
        public async Task<AccountResponseModel> Withdraw(TransactionrequestModel requestModel)
        {
            var query = _appDbContext.TblAccounts.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.AccountNo == requestModel.AccountNo);
            if (item is null)
            {
                throw new Exception("Account is not found.");
            }
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                item.Balance -= requestModel.Amount;
                _appDbContext.TblAccounts.Update(item);
                int result = await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }

            AccountResponseModel model = new AccountResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Deposit Successfully.")
            };
            return model;
        }
        #endregion

        public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
        {
            TransferResponseModel model = new TransferResponseModel();
            var query = _appDbContext.TblAccounts.AsNoTracking();
            var fromAccount=await query.FirstOrDefaultAsync(x=>x.AccountNo == requestModel.FromAccountNo);
            if(fromAccount is null)
            {
                throw new Exception("Invalid From Account");
            }

            var toAccount=await query.FirstOrDefaultAsync(x=>x.AccountNo==requestModel.ToAccountNo);
            if (toAccount is null)
            {
                throw new Exception("Invalid To Account");
            }

            if (fromAccount.Balance < requestModel.Amount)
            {
                model = new TransferResponseModel()
                {
                    Response = new MessageResponseModel(false, "Insufficient Balance.")
                };
                goto result;
            }

            var transacion= await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                fromAccount.Balance -= requestModel.Amount;
                _appDbContext.TblAccounts.Update(fromAccount);

                toAccount.Balance += requestModel.Amount;
                _appDbContext.TblAccounts.Update(toAccount);
                TblTransactionHistory creditTransactionHistory = new TblTransactionHistory()
                {
                    Amount = requestModel.Amount,
                    TransactionDate = DateTime.Now,
                    FromAccountNo = fromAccount.AccountNo!,
                    ToAccountNo = toAccount.AccountNo!,
                    AdminUserCode = "Admin",
                    TransactionType = "Credit"
                };


                TblTransactionHistory debitTransactionHistory = new TblTransactionHistory()
                {
                    Amount = requestModel.Amount,
                    TransactionDate = DateTime.Now,
                    ToAccountNo = toAccount.AccountNo!,
                    FromAccountNo = fromAccount.AccountNo!,
                    AdminUserCode = "Admin",
                    TransactionType = "Debit"
                };
                await _appDbContext.AddRangeAsync(debitTransactionHistory, creditTransactionHistory);
                await _appDbContext.SaveChangesAsync();

                await transacion.CommitAsync();

            }
            catch (Exception ex) 
            {
                await transacion.RollbackAsync();
            }
            model = new TransferResponseModel
            {
                Response = new MessageResponseModel(true, "Balance transfer successful.")
            };

          result:
             return model;

        }


    }
}
