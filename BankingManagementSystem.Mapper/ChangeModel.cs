﻿using BankingManagementSystem.DbService.Model;
using BankingManagementSystem.Models.Setup.Account;
using BankingManagementSystem.Models.Setup.AdminUser;
using BankingManagementSystem.Models.Setup.State;
using BankingManagementSystem.Models.Setup.Township;
using BankingManagementSystem.Models.Setup.TransactionHistory;
using BankingManagementSystem.Models.Setup.User;
//using DotNet8.BankingManagementSystem.Models.Account;

namespace BankingManagementSystem.Mapper
{
    public static class ChangeModel
    {
        public static StateModel Change(this TblPlaceState item)
        {
            return new StateModel()
            {
                StateCode = item.StateCode,
                StateName = item.StateName,
                StateId = item.StateId,
            };
        }

        #region State

        public static TblPlaceState Change(this StateRequestModel item)
        {
            var model = new TblPlaceState()
            {
                StateId = item.StateId,
                StateCode = item.StateCode,
                StateName = item.StateName,
            };
            return model;
        }
        #endregion

        #region Township

        public static TownshipModel Change(this TblPlaceTownship item)
        {
            return new TownshipModel()
            {
                TownshipId = item.TownshipId,
                TownshipCode = item.TownshipCode,
                TownshipName = item.TownshipName,
                StateCode = item.StateCode,
            };
        }

        public static TblPlaceTownship Change(this TownshipRequestModel item)
        {
            var model = new TblPlaceTownship()
            {
                TownshipId = item.TownshipId,
                TownshipCode = item.TownshipCode,
                TownshipName = item.TownshipName,
                StateCode = item.StateCode
            };
            return model;
        }

        #endregion

        #region Users

        public static UserModel Change(this TblUser item)
        {
            var model = new UserModel()
            {
                UserId = item.UserId,
                UserCode = item.UserCode,
                UserName = item.UserName,
                CustomerId = item.CustomerId,
                FullName = item.FullName,
                Email = item.Email,
                Address = item.Address,
                MobileNo = item.MobileNo,
                Nrc = item.Nrc,
                StateCode = item.StateCode,
                TownshipCode = item.TownshipCode,
            };
            return model;
        }
        public static UserModel Change(this UserRequestModel item)
        {
            return new UserModel()
            {
                UserName = item.UserName,
                FullName = item.FullName,
                Email = item.Email,
                Address = item.Address,
                MobileNo = item.MobileNo,
                Nrc = item.Nrc,
                StateCode = item.StateCode,
                TownshipCode = item.TownshipCode
            };
        }
        #endregion

        #region Account

        public static TblAccount Change(this AccountRequestModel item)
        {
            var model = new TblAccount()
            {
                AccountNo =item.AccountNo,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                Balance = item.Balance,
            };
            return model;
        }

        public static AccountModel Change(this TblAccount item)
        {
            return new AccountModel()
            {
                AccountId = item.AccountId,
                AccountNo = item.AccountNo,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                Balance = item.Balance
                
            };
        }

        #endregion

        #region AdminUser

        public static AdminUserModel Change(this TblAdminUser item)
        {
            return new AdminUserModel()
            {
                AdminUserId = item.AdminUserId,
                AdminUserCode = item.AdminUserCode,
                AdminUserName = item.AdminUserName,
                MobileNo = item.MobileNo,
                UserRoleCode = item.UserRoleCode
            };
        }

        public static TblAdminUser Change(this AdminUserRequestModel item)
        {
            var model = new TblAdminUser
            {
                AdminUserCode = item.AdminUserCode,
                AdminUserName = item.AdminUserName,
                MobileNo = item.MobileNo,
                UserRoleCode = item.UserRoleCode
            };
            return model;
        }

        #endregion

        #region TransactionHistory

        public static TransactionHistoryModel Change(this TblTransactionHistory item)
        {
            return new TransactionHistoryModel()
            {
                Amount = item.Amount,
                TransactionDate = item.TransactionDate,
                TransactionType = item.TransactionType,
                AdminUserCode = item.AdminUserCode,
                FromAccountNo = item.FromAccountNo,
                ToAccountNo = item.ToAccountNo,
            };
        }

        #endregion
    }
}
