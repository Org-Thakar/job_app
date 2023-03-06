using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities
{
    public class LocationRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }

    
}
