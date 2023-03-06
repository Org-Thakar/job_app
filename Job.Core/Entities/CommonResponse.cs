using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities
{ 
     public class CommonResponse
    {

        public string errormessage { get; set; }

        public bool success { get; set; }

        public string data { get; set; }
    }
}
