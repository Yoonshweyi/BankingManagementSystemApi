using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Services.Features.Generate
{
    public class GenerateService
    {
        private readonly AppDbContext _appDbContext;

        public GenerateService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<TblAccount>> GenerateAccounts(int count, int year)
        {
            Random random = new Random();
             List<TblAccount> model = [];

            for (int i = 0; i < count; i++)
            {
                
                    string customerCode = GenerateCustomerCode();
                    string customerName= GenerateCustomerName();
                    string accountNo = GenerateIBAN();
                    decimal balance = (decimal)(random.Next(10000000, 100000000));
                    if (balance < 0) balance *= -1;
                    int num = balance.ToString().Length == 8 ? 2 : 3;
                    balance = Convert.ToDecimal(balance.ToString().Substring(0, num)) * 100000;
                    TblAccount item = new TblAccount
               {
                    CustomerName = customerName,
                    CustomerCode = customerCode,
                    AccountNo=accountNo,
                    Balance = balance
                };
                model.Add(item);
                
            }
            await _appDbContext.TblAccounts.AddRangeAsync(model);
            await _appDbContext.SaveChangesAsync();

            List<TblTransactionHistory> transactions = [];
            foreach (var item in model)
            {
                DateTime startDate = new DateTime(year, 01, 01);
                DateTime endDate = new DateTime(year, 01, 31);
                for (DateTime date = startDate.Date; endDate.CompareTo(date) >= 0; date = date.AddDays(1))
                {
                    string GetDifferentAccountNo(int currentAccountId)
                    {
                        Random random = new Random();
                        int randomNumber;
                        do
                        {
                            randomNumber = random.Next(1, 100);
                        } while (randomNumber == currentAccountId);

                        return randomNumber.ToString("D6");
                    }

                    var fromAccountNo = GetDifferentAccountNo(item.AccountId);
                    var toAccountNo = (Convert.ToInt32(fromAccountNo) + 1).ToString("D6");
                    var amount = (decimal)random.NextDouble() * 1000000;
                    TblTransactionHistory creditTransaction = new TblTransactionHistory
                    {
                        FromAccountNo = fromAccountNo,
                        ToAccountNo = toAccountNo,
                        TransactionDate = date,
                        Amount = amount,
                        AdminUserCode="Admin",
                        TransactionType="Credit"
                    };
                    //transactions.Add(creditTransaction);
                    transactions.Add(creditTransaction);

                    TblTransactionHistory debitTransaction = new TblTransactionHistory
                    {
                        FromAccountNo = fromAccountNo,
                        ToAccountNo = toAccountNo,
                        TransactionDate = date,
                        Amount = amount,
                        AdminUserCode = "Admin",
                        TransactionType = "Debit"
                    };
                    transactions.Add(debitTransaction);
                }
            }
            await _appDbContext.TblTransactionHistories.AddRangeAsync(transactions);
            await _appDbContext.SaveChangesAsync();
            return model;
        }

        private string GenerateCustomerCode()
        {
            string randomNumber = new Random().Next(1000000, 9999999).ToString();
            return "C" + randomNumber;
        }

        private static string GenerateCustomerName()
        {
            string[] firstNames = ["John", "Alice", "Michael", "Emily", "David", "Sarah"];

            Random rand = new Random();
            string name = firstNames[rand.Next(firstNames.Length)];

            return name;
        }

        private string GenerateIBAN()
        {
            Random random = new Random();

            // Example format: CountryCode (2 letters) + CheckDigits (2 digits) + AccountNumber (22 digits)
            StringBuilder ibanBuilder = new StringBuilder();

            // Generate random country code (2 uppercase letters)
            ibanBuilder.Append(GetRandomLetters(2));

            // Add random check digits (2 digits)
            ibanBuilder.Append(random.Next(10));
            ibanBuilder.Append(random.Next(10));

            // Add random account number (22 digits)
            for (int i = 0; i < 22; i++)
            {
                ibanBuilder.Append(random.Next(10));
            }

            return ibanBuilder.ToString();
        }

        private string GetRandomLetters(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }

}
