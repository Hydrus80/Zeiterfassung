using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                if ((string.IsNullOrEmpty(username)) && (string.IsNullOrEmpty(password)))
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

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}