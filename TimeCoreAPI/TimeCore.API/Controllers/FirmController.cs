using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;
using TimeCore.ModelService;

namespace TimeCore.API.Controllers
{
    //http://localhost:XXXX/api/Firm
    [Route("api/[controller]")]
    [ApiController]
    public class FirmController : ControllerBase
    {
        public IFirmModelService firmModelService;

        public IFirmModelService GetCurrentFirmSQLRepository()
        {
            if (firmModelService is null)
                firmModelService = new FirmModelService(SupportedDatabaseType.eDatabaseType.SQL);
            return firmModelService;
        }

        ///http://localhost:8558/api/Firm/GetFirmByID/3
        [HttpGet]
        [Route("GetFirmByID/{firmID}")]
        public IActionResult GetFirmByID(int firmID)
        {
            try
            {
                if (firmID == 0)
                    return BadRequest();
                FirmModel foundFirm = (FirmModel)GetCurrentFirmSQLRepository().GetFirmByID(firmID);
                if (foundFirm is FirmModel)
                    return new OkObjectResult(foundFirm);
                else
                    return new NotFoundObjectResult(new FirmModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmController.GetFirmByID(): {ex.Message}");
                return StatusCode(500);
            }
        }


        ///http://localhost:8558/api/Firm/Async/GetFirmByID/3
        [HttpGet]
        [Route("Async/GetFirmByID/{firmID}")]
        public async Task<IActionResult> GetFirmByID_Async(int firmID)
        {
            try
            {
                if (firmID == 0)
                    return BadRequest();
                FirmModel foundFirm = (FirmModel)await GetCurrentFirmSQLRepository().GetFirmByID_Async(firmID).ConfigureAwait(false);
                if (foundFirm is FirmModel)
                    return new OkObjectResult(foundFirm);
                else
                    return new NotFoundObjectResult(new FirmModel());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmController.GetFirmByID_Async(): {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
