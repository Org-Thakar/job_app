using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities
{
    public class JobRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int DeaprtmentId { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
