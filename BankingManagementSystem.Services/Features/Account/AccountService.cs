using BankingManagementSystem.Models.Setup.Account;
//using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Shared;
using System.Text;
using BankingManagementSystem.Models.Setup;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BankingManagementSystem.Services.Features.Account
{
    public class AccountService
    {
        private readonly AppDbContext _dbContext;

        public AccountService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region get Account list
        public async Task<AccountListResponseModel> GetAccounts()
        {
            var query = _dbContext.TblAccounts.AsNoTracking();
            var result = await query.ToListAsync();
            var lst = result.Select(x => x.Change()).ToList();
            AccountListResponseModel model = new AccountListResponseModel()
            {
                Data = lst,
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }

        #endregion

        #region get Account by AccountNo
        public async Task<AccountResponseModel> GetAccont(string accountNo)
        {
            var query = _dbContext.TblAccounts.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
            if (item is null)
            {
                throw new Exception("Invalid Account");
            }
            var model = new AccountResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Account Success")

            };
            
            return model;
           
        }
        #endregion
        public async Task<AccountListResponseModel> GetAccountList(int pageNo,int pageSize)
        {
            var query = _dbContext.TblAccounts.AsNoTracking();
            var result = await query
            .OrderByDescending(x => x.AccountId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            var count = await query.CountAsync();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;
            var lst = result.Select(x => x.Change()).ToList();
            AccountListResponseModel model = new AccountListResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;


        }
        #region pagination

        #endregion

        #region Account create
        public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel)
        {
            var item=requestModel.Change();
            item.AccountNo = GenerateBankAccountNo();
            await _dbContext.TblAccounts.AddAsync(item);
            var result=await _dbContext.SaveChangesAsync();
            AccountResponseModel model = new AccountResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Account has created successfully.")
            };
            return model;
        }
        #endregion

        #region update account
        public async Task<AccountResponseModel> UpdateAccount(string accountNo,AccountRequestModel reqmodel)
        {
            var query=_dbContext.TblAccounts.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.AccountNo==accountNo);
            if (item is null) 
            {
                throw new Exception("Invalid Account");
            }

            item.CustomerCode = reqmodel.CustomerCode;
            item.CustomerName = reqmodel.CustomerName;
            item.Balance = reqmodel.Balance;
            _dbContext.Entry(item).State=EntityState.Modified;
            _dbContext.TblAccounts.Update(item);
            var result = await _dbContext.SaveChangesAsync();
            AccountResponseModel model = new AccountResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Account has updated Successfully.")
            };
            return model;
        }
        #endregion

        #region delete account
        public async Task<AccountResponseModel> DeleteAccount(string accountNo)
        {
            var query = _dbContext.TblAccounts.AsNoTracking();
            var item=await query.FirstOrDefaultAsync( x=>x.AccountNo==accountNo);
            if(item is null)
            {
                throw new Exception("Invalid Account");
            }
            _dbContext.Entry(item).State = EntityState.Deleted;
            _dbContext.TblAccounts.Remove(item);
            var result=await _dbContext.SaveChangesAsync();
            AccountResponseModel model = new AccountResponseModel()
            {
                Response=new MessageResponseModel(true,"Account has deleted successfully.")

            };
            return model;
        }

        #endregion


        public static string GenerateBankAccountNo()
        {
            Random random = new Random();
            int accountNumber = random.Next(100000, 999999); // Generates a number between 100000 and 999999
            string formattedAccountNumber = accountNumber.ToString("D6");
            return formattedAccountNumber;
        }

        //private string GenerateIBAN()
        //{
        //    Random random = new Random();

        //    // Example format: CountryCode (2 letters) + CheckDigits (2 digits) + AccountNumber (22 digits)
        //    StringBuilder ibanBuilder = new StringBuilder();

        //    // Generate random country code (2 uppercase letters)
        //    ibanBuilder.Append(GetRandomLetters(2));

        //    // Add random check digits (2 digits)
        //    ibanBuilder.Append(random.Next(10));
        //    ibanBuilder.Append(random.Next(10));

        //    // Add random account number (22 digits)
        //    for (int i = 0; i < 22; i++)
        //    {
        //        ibanBuilder.Append(random.Next(10));
        //    }

        //    return ibanBuilder.ToString();
        //}

        //private string GetRandomLetters(int length)
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    return new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}


    }
}
