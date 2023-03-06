using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities
{
    public class JobListResponse
    {
        public int Total { get; set; }
        public List<JobDetailsResp> Data { get; set; }
    }
    public class JobDetailsResp
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }

        public string Department { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
       
    }
}
