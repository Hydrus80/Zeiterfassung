using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;
using TimeCore.ModelService;
using TimeCore.ModelService.EFC.SQL;
using TimeCore.ModulService;

namespace TimeCore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimeCoreController : ControllerBase
    {
        //Felder
        public ITimeCoreModulService timeCoreModulService;

        public ITimeCoreModulService GetCurrentTimeCoreSQLModulService()
        {
            if (timeCoreModulService is null)
                timeCoreModulService = new TimeCoreSQLModulService(
                    new TimeStampModelService(SupportedDatabaseType.eDatabaseType.SQL,new TimeStampSQLRepository()), 
                    new AccountModelService(SupportedDatabaseType.eDatabaseType.SQL, new AccountSQLRepository()));
            return timeCoreModulService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SQL/Login/{accountUserName}/{accountPassword}/{workshopID}")]
        public IActionResult Login(string accountUserName, string accountPassword, int workshopID)
        {
            try
            {
                if ((string.IsNullOrEmpty(accountUserName)) && (string.IsNullOrEmpty(accountPassword)) && (workshopID == 0))
                    return BadRequest();
                AccountModel foundResult = GetCurrentTimeCoreSQLModulService().Login(accountUserName, accountPassword, workshopID);
                if (foundResult is AccountModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new AccountModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.Login(): {ex.Message}");
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SQL/Async/Login/{accountUserName}/{accountPassword}/{workshopID}")]
        public async Task<IActionResult> Login_Async(string accountUserName, string accountPassword, int workshopID)
        {
            try
            {
                if ((string.IsNullOrEmpty(accountUserName)) && (string.IsNullOrEmpty(accountPassword)) && (workshopID == 0))
                    return BadRequest();
                AccountModel foundResult = await GetCurrentTimeCoreSQLModulService().Login_Async(accountUserName, accountPassword, workshopID).ConfigureAwait(false);
                if (foundResult is AccountModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new AccountModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.Login_Async(): {ex.Message}");
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] AccountModel accountModel)
        {
            try
            {
                if ((string.IsNullOrEmpty(accountModel.Username)) && (string.IsNullOrEmpty(accountModel.Password)) && (accountModel.Workshop.ID == 0))
                    return BadRequest();
                AccountModel foundResult = GetCurrentTimeCoreSQLModulService().Login(accountModel.Username, accountModel.Password, accountModel.Workshop.ID);
                if (foundResult is AccountModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new AccountModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.Authenticate(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/StampIn/{userAccount}/{timeStampYear}/{timeStampMonth}/{timeStampDay}/{timeStampHour}/{timeStampMinute}/{timeStampSecond}")]
        public IActionResult StampIn(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            try
            {
                if((userAccount is null) && (timeStampYear == 0) && (timeStampMonth == 0) && (timeStampDay == 0))
                    return BadRequest();
                TimeStampModel foundResult = (TimeStampModel)GetCurrentTimeCoreSQLModulService().StampIn(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new TimeStampModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampIn(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/Async/StampIn/{userAccount}/{timeStampYear}/{timeStampMonth}/{timeStampDay}/{timeStampHour}/{timeStampMinute}/{timeStampSecond}")]
        public async Task<IActionResult> StampIn_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            try
            {
                if ((userAccount is null) && (timeStampYear == 0) && (timeStampMonth == 0) && (timeStampDay == 0))
                    return BadRequest();
                TimeStampModel foundResult = await GetCurrentTimeCoreSQLModulService().StampIn_Async(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new TimeStampModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampIn(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/StampOut/{userAccount}/{timeStampYear}/{timeStampMonth}/{timeStampDay}/{timeStampHour}/{timeStampMinute}/{timeStampSecond}")]
        public IActionResult StampOut(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            try
            {
                if ((userAccount is null) && (timeStampYear == 0) && (timeStampMonth == 0) && (timeStampDay == 0))
                    return BadRequest();
                TimeStampModel foundResult = (TimeStampModel)GetCurrentTimeCoreSQLModulService().StampOut(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new TimeStampModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampOut(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/Async/StampOut/{userAccount}/{timeStampYear}/{timeStampMonth}/{timeStampDay}/{timeStampHour}/{timeStampMinute}/{timeStampSecond}")]
        public async Task<IActionResult> StampOut_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            try
            {
                if ((userAccount is null) && (timeStampYear == 0) && (timeStampMonth == 0) && (timeStampDay == 0))
                    return BadRequest();
                TimeStampModel foundResult = await GetCurrentTimeCoreSQLModulService().StampOut_Async(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
                if (foundResult is TimeStampModel)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new TimeStampModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.StampOut(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/GetStampTimesMonthList/{accountUserName}/{selectedYear}/{selectedMonth}")]
        public IActionResult GetStampTimesMonthList(AccountModel accountUserName, int selectedYear, int selectedMonth)
        {
            try
            {
                if ((accountUserName is null) && (selectedYear == 0) && (selectedMonth == 0))
                    return BadRequest();
                List<TimeStampModel> foundResult = GetCurrentTimeCoreSQLModulService().GetStampTimesMonthList(accountUserName, selectedYear, selectedMonth);
                if (foundResult is List<TimeStampModel>)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new List<TimeStampModel>());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetCurrentTimeCoreSQLModulService(): {ex.Message}");
                return StatusCode(500);
            }
        }

        //http://localhost:8558/api/TimeCore/Login/
        [HttpGet]
        [Route("SQL/GetStampTimesMonthList/{accountUserName}/{selectedYear}/{selectedMonth}")]
        public async Task<IActionResult> GetStampTimesMonthList_Async(AccountModel accountUserName, int selectedYear, int selectedMonth)
        {
            try
            {
                if ((accountUserName is null) && (selectedYear == 0) && (selectedMonth == 0))
                    return BadRequest();
                List<TimeStampModel> foundResult = await GetCurrentTimeCoreSQLModulService().GetStampTimesMonthList_Async(accountUserName, selectedYear, selectedMonth).ConfigureAwait(false);
                if (foundResult is List<TimeStampModel>)
                    return new OkObjectResult(foundResult);
                else
                    return new NotFoundObjectResult(new List<TimeStampModel>());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeCoreController.GetCurrentTimeCoreSQLModulService(): {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
