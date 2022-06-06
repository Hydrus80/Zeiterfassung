using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
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

        /*POST http://localhost:8558/api/TimeCore/SQL/Authenticate
        {
            "requestUserName": "max",
            "requestPassword": "max"
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

        /*POST http://localhost:8558/api/TimeCore/SQL/AuthenticateAsync
        {
            "requestUserName": "max",
            "requestPassword": "max"
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

        /*GET http://localhost:8558/api/TimeCore/SQL/GetStampTimesMonthList
        {
            "requestYear": 2022,
            "requestMonth": 6
        }
        Authentization -> Bearer Token
       */
        [HttpGet]
        [Route("SQL/GetStampTimesMonthList")]
        public IActionResult GetStampTimesMonthList([FromBody] RequestModel requestModel)
        {
            try
            {
                requestModel = requestModulService.GetAuthenticatedUser(this.User, requestModel);
                if (string.IsNullOrEmpty(requestModel.requestGUID))
                    return BadRequest();
                List<TimeStampModel> foundResult = requestModulService.GetStampTimesMonthList(requestModel);
                if (foundResult is List<TimeStampModel>)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetStampTimesMonthList(): {ex.Message}");
                return StatusCode(500);
            }
        }

        /*GET http://localhost:8558/api/TimeCore/SQL/StampIn
        {
            "requestYear": 2022,
            "requestMonth": 6
            "requestDay": 6,
            "requestHour": 15
            "requestMinute": 58,
            "requestSecond": 23
        }
        Authentization -> Bearer Token
        */
        [HttpGet]
        [Route("SQL/StampIn")]
        public IActionResult StampIn([FromBody] RequestModel requestModel)
        {
            try
            {
                requestModel = requestModulService.GetAuthenticatedUser(this.User, requestModel);
                if (string.IsNullOrEmpty(requestModel.requestGUID))
                    return BadRequest();
                TimeStampModel foundResult = requestModulService.StampIn(requestModel);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampIn(): {ex.Message}");
                return StatusCode(500);
            }
        }

        /*GET http://localhost:8558/api/TimeCore/SQL/StampIn
        {
            "requestYear": 2022,
            "requestMonth": 6
            "requestDay": 6,
            "requestHour": 15
            "requestMinute": 58,
            "requestSecond": 23
        }
        Authentization -> Bearer Token
        */
        [HttpGet]
        [Route("SQL/StampOut")]
        public IActionResult StampOut([FromBody] RequestModel requestModel)
        {
            try
            {
                requestModel = requestModulService.GetAuthenticatedUser(this.User, requestModel);
                if (string.IsNullOrEmpty(requestModel.requestGUID))
                    return BadRequest();
                TimeStampModel foundResult = requestModulService.StampOut(requestModel);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampOut(): {ex.Message}");
                return StatusCode(500);
            }
        }


    }
}
