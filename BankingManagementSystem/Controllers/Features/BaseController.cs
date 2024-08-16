using BankingManagementSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystemBackendApi.Controllers.Features
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult InternalServerError(Exception exception)
        {
            return Ok(new
            {
                Response = new MessageResponseModel(false, exception)
            });
        }
    }
}
