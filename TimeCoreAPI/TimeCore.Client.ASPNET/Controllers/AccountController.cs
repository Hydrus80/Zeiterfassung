using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimeCore.API;
using TimeCore.ErrorHandler;

namespace TimeCore.Client.ASPNET
{
    [Route("account")]
    public class AccountController : Controller
    {
        //Felder
        public IRequestModulService requestModulService;

        public AccountController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //https://learningprogramming.net/net/asp-net-core-mvc-5/login-form-with-session-in-asp-net-core-mvc-5/
            try
            {
                //Init
                List<TimeStampModel> returnList = null;

                if ((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(password)))
                    return BadRequest();
                string foundResult = requestModulService.Authenticate(new RequestModel() { requestUserName = username, requestPassword = password });
                if (!string.IsNullOrEmpty(foundResult))
                {
                    //User setzen
                    HttpContext.Session.SetString("userGUID", foundResult);
                    HttpContext.Session.SetString("userUserName", username);

                    //Tagesliste holen
                    returnList = requestModulService.GetStampTimesList(new RequestModel() { requestGUID = foundResult, requestYear = DateTime.Now.Year, requestMonth = DateTime.Now.Month, requestDay = DateTime.Now.Day });

                    //Liste gefunden?
                    if (returnList is List<TimeStampModel>)
                        return View("~/Views/TimeCore/Stamp.cshtml", returnList);
                    else
                        return View("~/Views/TimeCore/Stamp.cshtml", new List<TimeStampModel>());
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountController.Login(): {ex.Message}");
                return StatusCode(500);
            }

        }

        [Route("remotelogin")]
        [HttpPost]
        public async Task<IActionResult> RemoteLogin(string username, string password, string serverport)
        {
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            try
            {
                if ((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(password)) || (string.IsNullOrEmpty(serverport)))
                    return BadRequest();

                // Update port # in the following line.
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"http://localhost:{serverport}/ ");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/TimeCore/SQL/Authenticate", new RequestModel() { requestUserName = username, requestPassword = password }).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string APIresponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(APIresponse))
                    {
                        HttpContext.Session.SetString("userGUID", APIresponse);
                        HttpContext.Session.SetString("userUserName", username);
                        return View("Success");
                    }
                    else
                    {
                        ViewBag.error = "Invalid Account";
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountController.RemoteLogin(): {ex.Message}");
                return StatusCode(500);
            }
        }

        [Route("openremote")]
        [HttpPost]
        public IActionResult OpenRemote()
        {
            return View("RemoteIndex");
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userGUID");
            HttpContext.Session.Remove("userUserName");
            return RedirectToAction("Index");
        }
    }
}