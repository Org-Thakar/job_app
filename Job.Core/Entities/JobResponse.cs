using System;
using System.Collections.Generic;
using System.Text;
using Job.Core.Entities.Base;

namespace Job.Core.Entities
{
        public class JobResponse : BaseResponse
        {
            public bool Success { get; set; }
            public int Id { get; set; }
    }
  
}
