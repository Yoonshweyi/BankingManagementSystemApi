using BankingManagementSystem.Models.Setup.User;
using BankingManagementSystem.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingManagementSystem.DbService.Model;

namespace BankingManagementSystem.Services.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        #region Get Users
        public async Task<UserListResponseModel> GetUserList()
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var result= await query
                .OrderByDescending(x => x.UserId)
                .ToListAsync();
            var lst=result.Select(x=>x.Change()).ToList();

            UserListResponseModel model = new UserListResponseModel()
            {
                Data = lst,
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion

        #region Get User By UserCode
        public async Task<UserResponseModel> GetUserByUserCode(string userCode)
        {
            var query=_appDbContext.TblUsers.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.UserCode==userCode);
            UserResponseModel model = new UserResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion


        #region Create User
        public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel)
        {
            var item = new TblUser()
            {
                UserName = requestModel.UserName,
                FullName = requestModel.FullName,
                Email = requestModel.Email,
                Address = requestModel.Address,
                MobileNo = requestModel.MobileNo,
                Nrc = requestModel.Nrc,
                StateCode = requestModel.StateCode,
                TownshipCode = requestModel.TownshipCode
            };
            var userCode = GenerateUniqueUserCode();
            item.UserCode = userCode;
            await _appDbContext.TblUsers.AddAsync(item);
            var result = await _appDbContext.SaveChangesAsync();

            UserResponseModel model = new UserResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "User has created successfully.")
            };
            return model;
        }
        #endregion

        #region Update User info

        public async Task<UserResponseModel> UpdateUserInfo(UserRequestModel requestModel)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.UserCode == requestModel.UserCode);
            if (item is null)
            {
                throw new Exception("User is null.");
            }
            if (string.IsNullOrEmpty(requestModel.UserCode)) throw new Exception("UserCode is required.");
            if (string.IsNullOrEmpty(requestModel.UserName)) throw new Exception("UserName is required.");
            if (string.IsNullOrEmpty(requestModel.FullName)) throw new Exception("FullName is required.");
           // if (string.IsNullOrEmpty(requestModel.Nrc)) throw new Exception("Nrc is required.");
           // if (string.IsNullOrEmpty(requestModel.Address)) throw new Exception("Address is required.");
           // if (string.IsNullOrEmpty(requestModel.Email)) throw new Exception("Email is required.");
            item.UserCode = requestModel.UserCode;
            item.UserName = requestModel.UserName;
            item.FullName = requestModel.FullName;
            item.Nrc = requestModel.Nrc;
            item.Address = requestModel.Address;
            item.Email = requestModel.Email;
            item.StateCode = requestModel.StateCode;
            item.TownshipCode = requestModel.TownshipCode;

            _appDbContext.Entry(item).State = EntityState.Modified;
            _appDbContext.TblUsers.Update(item);
            var result = await _appDbContext.SaveChangesAsync();

            UserResponseModel model = new UserResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "User information have updated successfully.")
            };
            return model;
        }

        #endregion

        #region Delete User

        public async Task<UserResponseModel> DeleteUser(string userCode)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.UserCode == userCode);
            if (item is null)
            {
                throw new Exception("User is null.");
            }

            _appDbContext.Entry(item!).State = EntityState.Deleted;
            _appDbContext.TblUsers.Remove(item!);
            var result = await _appDbContext.SaveChangesAsync();

            UserResponseModel model = new UserResponseModel
            {
                Response = new MessageResponseModel(true, "User has deleted successfully.")
            };
            return model;
        }

        #endregion


        #region Generate user codes

        private string GenerateUniqueUserCode()
        {
            string latestUserCode = "AB000";

            if (int.TryParse(latestUserCode.Substring(2), out int numericPart))
            {
                numericPart++;

                while (IsUserCodeAlreadyUsed("AB" + numericPart.ToString("D3")))
                {
                    numericPart++;
                }

                return "AB" + numericPart.ToString("D3");
            }

            return "AB000";
        }

        private bool IsUserCodeAlreadyUsed(string userCode)
        {
            return _appDbContext.TblUsers.Any(x => x.UserCode == userCode);
        }

        #endregion





    }


}
