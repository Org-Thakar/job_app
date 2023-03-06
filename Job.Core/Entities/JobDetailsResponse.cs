using System;

namespace Job.Core.Entities
{
    public class JobDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Location LocationDetails { get; set; }
        public Department DepartmentDetails { get; set; }
        
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
