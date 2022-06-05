using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.API
{
    public class RequestModel
    {
        public string requestUserName { get; set; }
        public string requestPassword { get; set; }
        public string requestGUID { get; set; }
        public string requestSourceType { get; set; }
    }
}
