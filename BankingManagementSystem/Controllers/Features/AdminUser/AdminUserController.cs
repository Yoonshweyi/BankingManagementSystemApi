using BankingManagementSystem.DbService.Model;
using BankingManagementSystem.Mapper;
using BankingManagementSystem.Model;
using BankingManagementSystem.Models.Setup.Account;
using BankingManagementSystem.Models.Setup.AdminUser;
using BankingManagementSystem.Services.Features.AdminUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features.AdminUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : BaseController
    {
        private readonly AdminUserService _adminUserService;

        public AdminUserController(AdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }
        #region GetAdminUsers
        [HttpGet]
        public async Task<IActionResult> GetAdminUsers()
        {
            try
            {
                var model = await _adminUserService.GetAdminUsers();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }
        #endregion

        #region GetAdminUserByCode
        [HttpGet("{AdminUserCode}")]
        public async Task<IActionResult> GetAdminUserByCode(string adminUserCode)
        {
            try
            {
                var model = await _adminUserService.GetAdminUser(adminUserCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            #endregion
        }

        #region CreateAdminUser
        [HttpPost]
        public async Task<IActionResult> CreateAdminUser([FromBody] AdminUserRequestModel requestModel)
        {
            try
            {
                var model = await _adminUserService.CreateAdminUser(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            #endregion
        }


        #region UpdateAdminUser
        [HttpPut("{AdminUserCode}")]
        public async Task<IActionResult> UpdateAdminUser(string AdminUserCode, AdminUserRequestModel requestModel)
        {
            try
            {
                var model = await _adminUserService.UpdateAdminUser(AdminUserCode, requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region DeleteAdminUser
        [HttpDelete("{AdminUserCode}")]
        public async Task<IActionResult> DeleteAdminUser(string AdminUserCode)
        {
            try
            {
                var model = await _adminUserService.DeleteAdminUser(AdminUserCode);
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
