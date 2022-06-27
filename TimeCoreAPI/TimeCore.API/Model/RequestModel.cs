using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.API
{
    [BindProperties]
    public class RequestModel
    {
        public string requestUserName { get; set; }
        public string requestPassword { get; set; }
        public string requestGUID { get; set; }
        public int requestYear { get; set; }
        public int requestMonth { get; set; }
        public int requestDay { get; set; }
        public int requestHour { get; set; }
        public int requestMinute { get; set; }
        public int requestSecond { get; set; }
        public string requestSourceType { get; set; }
    }
}
