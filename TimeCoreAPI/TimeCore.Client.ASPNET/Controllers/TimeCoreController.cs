using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeCore.API;
using TimeCore.ErrorHandler;

namespace TimeCore.Client.ASPNET
{
    [Route("timecore")]
    public class TimeCoreController : Controller
    {
        //Felder
        public IRequestModulService requestModulService;

        public TimeCoreController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
        }

        //https://www.c-sharpcorner.com/article/asp-net-mvc-passing-data-from-controller-to-view/
        [Route("userlist")]
        [HttpGet]
        public IActionResult GetUserList(string listSelection)
        {
            try
            {
                //Init
                List<TimeStampModel> foundResult = null;

                //Parameter vorhanden?
                if ((string.IsNullOrEmpty(HttpContext.Session.GetString("userGUID"))) || (string.IsNullOrEmpty(listSelection)))
                    return BadRequest();

                //Monat
                if (listSelection == "month")
                    foundResult = requestModulService.GetStampTimesList(new RequestModel() { requestGUID = HttpContext.Session.GetString("userGUID"), requestYear = DateTime.Now.Year, requestMonth = DateTime.Now.Month });
                else if (listSelection == "day")
                    foundResult = requestModulService.GetStampTimesList(new RequestModel() { requestGUID = HttpContext.Session.GetString("userGUID"), requestYear = DateTime.Now.Year, requestMonth = DateTime.Now.Month, requestDay = DateTime.Now.Day });

                //Liste gefunden?
                if (foundResult is List<TimeStampModel>)
                    return View(foundResult);
                else
                {
                    ViewBag.error = "Keine Werte gefunden";
                    return View(new List<TimeStampModel>());
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountController.GetUserList(): {ex.Message}");
                return StatusCode(500);
            }
        }

        [Route("stamp")]
        [HttpGet]
        public IActionResult Stamp()
        {
            try
            {
                //Init
                List<TimeStampModel> listSelection = null;
                TimeStampModel foundResult = null;

                //Parameter vorhanden?
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("userGUID")))
                    return BadRequest();

                //Tagesliste holen
                listSelection = requestModulService.GetStampTimesList(new RequestModel() { requestGUID = HttpContext.Session.GetString("userGUID"), requestYear = DateTime.Now.Year, requestMonth = DateTime.Now.Month, requestDay = DateTime.Now.Day });
                if (listSelection is null)
                    return BadRequest();

                //Ein oder ausstempeln?
                TimeStampModel checkValue = listSelection.OrderByDescending(i => i.TimeStampHour).ThenByDescending(x => x.TimeStampMinute).ThenByDescending(x => x.TimeStampSecond).First();
                if(checkValue is null)
                    return BadRequest();
                else
                    if (checkValue.StampIn)
                        foundResult = requestModulService.StampOut(new RequestModel()
                        {
                            requestGUID = HttpContext.Session.GetString("userGUID"),
                            requestYear = DateTime.Now.Year,
                            requestMonth = DateTime.Now.Month,
                            requestDay = DateTime.Now.Day,
                            requestHour = DateTime.Now.Hour,
                            requestMinute = DateTime.Now.Minute,
                            requestSecond = DateTime.Now.Second
                        });
                    else
                        foundResult = requestModulService.StampIn(new RequestModel()
                        {
                            requestGUID = HttpContext.Session.GetString("userGUID"),
                            requestYear = DateTime.Now.Year,
                            requestMonth = DateTime.Now.Month,
                            requestDay = DateTime.Now.Day,
                            requestHour = DateTime.Now.Hour,
                            requestMinute = DateTime.Now.Minute,
                            requestSecond = DateTime.Now.Second,
                        });

                //Liste gefunden?
                if (foundResult is TimeStampModel)
                {
                    listSelection.Add(foundResult);
                    listSelection.OrderByDescending(i => i.TimeStampHour).ThenByDescending(x => x.TimeStampMinute).ThenByDescending(x => x.TimeStampSecond);
                    return View("Stamp", listSelection);
                }
                else
                {
                    ViewBag.error = "Stemplung war nicht erfolgreich";
                    return View("Stamp", listSelection);
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