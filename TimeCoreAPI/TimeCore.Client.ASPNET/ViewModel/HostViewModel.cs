using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TimeCore.Client.ASPNET.ViewModel
{
    public class HostViewModel
    {
        public string Host { get; set; }

        public List<SelectListItem> Hosts { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "http://localhost:8558", Text = "http://localhost:8558 (Debug)" },
            new SelectListItem { Value = "http://localhost:5000", Text = "http://localhost:5000 (Release)" },
        };
    }
}
