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
    [Route("timecore")]
    public class TimeCoreController : Controller
    {
        //Felder
        public IRequestModulService requestModulService;

        public TimeCoreController(IRequestModulService selRequestModulService)
        {
            requestModulService = selRequestModulService;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

    }
}