
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeCore.API.Handler;
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
        public IJSONHandler controllerJSONHandler;

        public TimeCoreController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
            controllerJSONHandler = new JSONHandler();
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

        /*GET http://localhost:8558/api/TimeCore/SQL/GetStampTimesList
        {
            "requestYear": 2022,
            "requestMonth": 6
        }
        Authentization -> Bearer Token
       */
        [HttpGet("GetStampTimesList")]
        [Route("SQL/GetStampTimesList")]
        public IActionResult GetStampTimesList([FromBody] RequestModel requestModel)
        {
            try
            {
                requestModel = requestModulService.GetAuthenticatedUser(this.User, requestModel);
                if (string.IsNullOrEmpty(requestModel.requestGUID))
                    return BadRequest();
                List<TimeStampModel> foundResult = requestModulService.GetStampTimesList(requestModel);
                if (foundResult is List<TimeStampModel>)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(string.Empty);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetStampTimesList(): {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET method
        [HttpGet("GetStampTimesList")]
        [Route("SQL/JSON/GetStampTimesList")]
        public IActionResult GetStampTimesList(string jsonData)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonData))
                    return BadRequest();
                else
                    return GetStampTimesList(controllerJSONHandler.ConvertJSONStringtoRequestModel(jsonData));
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetStampTimesList(): {ex.Message}");
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
        [HttpGet("StampIn")]
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

        // GET method
        [HttpGet("StampIn")]
        [Route("SQL/JSON/StampIn")]
        public IActionResult StampIn(string jsonData)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonData))
                    return BadRequest();
                else
                    return StampIn(controllerJSONHandler.ConvertJSONStringtoRequestModel(jsonData));
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

        // GET method
        [HttpGet("StampOut")]
        [Route("SQL/JSON/StampOut")]
        public IActionResult StampOut(string jsonData)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonData))
                    return BadRequest();
                else
                    return StampOut(controllerJSONHandler.ConvertJSONStringtoRequestModel(jsonData));
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampOut(): {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
