using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities
{
    public class JobListRequest
    {
        public string JobName { get; set; }
        public int PpageNo { get; set; }
        public int PageSize { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }

    
    }
}
