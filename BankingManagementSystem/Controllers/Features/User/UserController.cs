using BankingManagementSystem.DbService.Model;
using BankingManagementSystem.Model;
using BankingManagementSystem.Models.Setup.User;
using BankingManagementSystem.Services.Features.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystemBackendApi.Controllers.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService) 
        {
            _userService = userService;
        }
       
        #region Get Users
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
                var model = await _userService.GetUserList();
                return Ok(model);
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }
        #endregion


        #region Get User By UserCode
        [HttpGet("{userCode}")]
        public async Task<IActionResult> GetUserByCode(string userCode)
        {
            try
            {
                var model = await _userService.GetUserByUserCode(userCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Create User

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel requestModel)
        {
            try
            {
                var model = await _userService.CreateUser(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Update User info

        [HttpPut("{userCode}")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserRequestModel requestModel)
        {
            try
            {
                var model = await _userService.UpdateUserInfo(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Delete User

        [HttpDelete("{userCode}")]
        public async Task<IActionResult> DeleteUser(string userCode)
        {
            try
            {
                var model = await _userService.DeleteUser(userCode);
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
