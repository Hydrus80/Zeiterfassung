using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimeCore.API;
using TimeCore.Client.ASPNET.ModulService;
using TimeCore.ErrorHandler;

namespace TimeCore.Client.ASPNET
{
    [Route("timecore")]
    public class TimeCoreController : Controller
    {
        //Felder
        public IRequestModulService requestModulService;
        public IHTTPRequestBuilderModulService httpRequestBuilderModulService;

        public TimeCoreController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
        }

        public IHTTPRequestBuilderModulService GetCurrentHTTPRequestBuilderModulService()
        {
            if (httpRequestBuilderModulService is null)
                httpRequestBuilderModulService = new HTTPRequestBuilderModulService();
            return httpRequestBuilderModulService;
        }

        //https://www.c-sharpcorner.com/article/asp-net-mvc-passing-data-from-controller-to-view/
        [Route("GetUserList")]
        [HttpGet]
        public async Task<IActionResult> GetUserList(string listSelection)
        {
            //Init
            List<TimeStampModel> returnList = null;
            string APIresponse = string.Empty;
            RequestModel getRequestModel = new RequestModel();

            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            try
            {
                //GUID gesetzt?
                if ((string.IsNullOrEmpty(listSelection)) || (string.IsNullOrEmpty(HttpContext.Session.GetString("userGUID"))))
                    return BadRequest();
                else
                    APIresponse = HttpContext.Session.GetString("userGUID");

                //HTTPClient aufbauen
                HttpClient client = GetCurrentHTTPRequestBuilderModulService().CreateHttpClient(HttpContext.Session.GetString("hostURL"), APIresponse);
                getRequestModel.requestGUID = APIresponse;
                getRequestModel.requestYear = DateTime.Now.Year;
                getRequestModel.requestMonth = DateTime.Now.Month;

                //Tagesliste?
                if (listSelection == "day")
                    getRequestModel.requestDay = DateTime.Now.Day;
                
                //Schnittstelleabfragen
                returnList = await client.GetFromJsonAsync<List<TimeStampModel>>($"api/TimeCore/SQL/JSON/GetStampTimesList?jsonData=\"{JsonConvert.SerializeObject(getRequestModel)}").ConfigureAwait(false);

                //Liste gefunden?
                if (returnList is List<TimeStampModel>)
                    return View("~/Views/TimeCore/Stamp.cshtml", returnList);
                else
                    return View("~/Views/TimeCore/Stamp.cshtml", new List<TimeStampModel>());
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountController.GetUserList(): {ex.Message}");
                return StatusCode(500);
            }
        }

        [Route("Stamp")]
        [HttpGet]
        public async Task<IActionResult> Stamp()
        {
            try
            {
                //Init
                List<TimeStampModel> listSelection = null;
                TimeStampModel foundResult = null;
                RequestModel getRequestModel = new RequestModel();
                string APIresponse = string.Empty;
                TimeStampModel checkValue = new TimeStampModel();

                //Parameter vorhanden?
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userGUID")))
                    return BadRequest();
                else
                    APIresponse = HttpContext.Session.GetString("userGUID");

                //HTTPClient aufbauen
                HttpClient client = GetCurrentHTTPRequestBuilderModulService().CreateHttpClient(HttpContext.Session.GetString("hostURL"), APIresponse);
                getRequestModel.requestGUID = APIresponse;
                getRequestModel.requestYear = DateTime.Now.Year;
                getRequestModel.requestMonth = DateTime.Now.Month;
                getRequestModel.requestDay = DateTime.Now.Day;

                //Tagesliste holen
                //Schnittstelleabfragen
                listSelection = await client.GetFromJsonAsync<List<TimeStampModel>>($"api/TimeCore/SQL/JSON/GetStampTimesList?jsonData=\"{JsonConvert.SerializeObject(getRequestModel)}").ConfigureAwait(false);
                if (listSelection is null)
                    return BadRequest();

                //Liste vorhanden?
                if (listSelection.Count > 0)
                    checkValue = listSelection.OrderByDescending(i => i.TimeStampHour).ThenByDescending(x => x.TimeStampMinute).ThenByDescending(x => x.TimeStampSecond).First();
                else
                    checkValue.StampIn = true;

                //Ein/Ausstempeln
                if (checkValue is null)
                    return BadRequest();
                else
                {
                    if (checkValue.StampIn)
                    {
                        getRequestModel = new RequestModel()
                        {
                            requestGUID = HttpContext.Session.GetString("userGUID"),
                            requestYear = DateTime.Now.Year,
                            requestMonth = DateTime.Now.Month,
                            requestDay = DateTime.Now.Day,
                            requestHour = DateTime.Now.Hour,
                            requestMinute = DateTime.Now.Minute,
                            requestSecond = DateTime.Now.Second
                        };
                        foundResult = await client.GetFromJsonAsync<TimeStampModel>($"api/TimeCore/SQL/JSON/StampOut?jsonData=\"{JsonConvert.SerializeObject(getRequestModel)}").ConfigureAwait(false);
                    }
                    else
                    {
                        getRequestModel = new RequestModel()
                        {
                            requestGUID = HttpContext.Session.GetString("userGUID"),
                            requestYear = DateTime.Now.Year,
                            requestMonth = DateTime.Now.Month,
                            requestDay = DateTime.Now.Day,
                            requestHour = DateTime.Now.Hour,
                            requestMinute = DateTime.Now.Minute,
                            requestSecond = DateTime.Now.Second
                        };
                        foundResult = await client.GetFromJsonAsync<TimeStampModel>($"api/TimeCore/SQL/JSON/StampIn?jsonData=\"{JsonConvert.SerializeObject(getRequestModel)}").ConfigureAwait(false);
                    }
                }

                //Liste gefunden?
                if (listSelection is List<TimeStampModel>)
                {
                    listSelection.Add(foundResult);
                    listSelection.OrderByDescending(i => i.TimeStampHour).ThenByDescending(x => x.TimeStampMinute).ThenByDescending(x => x.TimeStampSecond);
                    return View("~/Views/TimeCore/Stamp.cshtml", listSelection);
                }
                else
                {
                    ViewBag.error = "Stemplung war nicht erfolgreich";
                    return View("~/Views/TimeCore/Stamp.cshtml", new List<TimeStampModel>());
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountController.Stamp(): {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}