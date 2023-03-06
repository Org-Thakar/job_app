using System;


namespace Job.Core.Entities
{
    public class JobRequestUpdate
    {
        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Country { get; set; }
        //public string Zip { get; set; }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }

        public int DeaprtmentId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
