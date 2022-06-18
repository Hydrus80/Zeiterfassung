using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
                if ((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(password)))
                    return BadRequest();
                string foundResult = requestModulService.Authenticate(new RequestModel() { requestUserName = username, requestPassword = password });
                if (!string.IsNullOrEmpty(foundResult))
                {
                    HttpContext.Session.SetString("userGUID", foundResult);
                    return View("Success");
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
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}