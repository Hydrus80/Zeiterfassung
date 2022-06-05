using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimeCoreController : ControllerBase
    {
        //Felder
        public IRequestModulService requestModulService;

        public TimeCoreController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
        }

        /*http://localhost:8558/api/TimeCore/SQL/Login/Account
        {
            "username": "max",
            "password": "max",
            "workshopID": 1
        }
         */
        [AllowAnonymous]
        [HttpPost]
        [Route("SQL/Authenticate")]
        public IActionResult Authenticate([FromBody] RequestModel requestModel)
        {
            try
            {
                if ((string.IsNullOrEmpty(requestModel.requestUserName)) && (string.IsNullOrEmpty(requestModel.requestPassword)))
                    return BadRequest();
                string foundResult = requestModulService.Authenticate(requestModel);
                if (!string.IsNullOrEmpty(foundResult))
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.Login(): {ex.Message}");
                return StatusCode(500);
            }
        }


        /*http://localhost:8558/api/TimeCore/SQL/LoginAsync
        {
            "username": "max",
            "password": "max",
            "workshopID": 1
        }
       */
        [AllowAnonymous]
        [HttpPost]
        [Route("SQL/AuthenticateAsync")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RequestModel requestModel)
        {
            try
            {
                if ((string.IsNullOrEmpty(requestModel.requestUserName)) && (string.IsNullOrEmpty(requestModel.requestPassword)))
                    return BadRequest();
                string foundResult = await requestModulService.AuthenticateAsync(requestModel).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(foundResult))
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.LoginAsync(): {ex.Message}");
                return StatusCode(500);
            }
        }


        /*http://localhost:8558/api/TimeCore/SQL/GetStampTimesMonthList
       {
           "accountUserName": "YYY",
           "accountPassword": "XXX",
           "workshopID": 1
       }
       */
        [HttpGet]
        [Route("SQL/GetStampTimesMonthList")]
        public IActionResult GetStampTimesMonthList()
        {
            try
            {
                //if (string.IsNullOrEmpty(requestModel.requestGUID))
                //    return BadRequest();
                //string foundResult = requestModulService.Authenticate(requestModel);
                //if (!string.IsNullOrEmpty(foundResult))
                //    return new OkObjectResult(foundResult);
                //else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetStampTimesMonthList(): {ex.Message}");
                return StatusCode(500);
            }
        }


    }
}
