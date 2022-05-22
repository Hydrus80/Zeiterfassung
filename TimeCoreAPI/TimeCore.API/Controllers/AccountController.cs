using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "First";
        }

        [HttpGet]
        [Route("list")]
        public List<string> GetList()
        {
            return new List<string>() {"First","Second" };
        }
    }
}
