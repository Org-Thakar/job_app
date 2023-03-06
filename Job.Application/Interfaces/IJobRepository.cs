using Job.Core.Entities;
using Job.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Application.Interfaces
{
    public interface IJobRepository : IRepository<JobResponse>
    {
   
        Task<JobDetailsResponse> GetJobById(int Id);
        Task<JobResponse> GetByIdAsync(Int64 id);
        //Task<JobResponse> GetCustomerByEmail(string email);
        Task<JobResponse> CreateJob(JobRequest request);
        Task<JobResponse> UpdateJob(JobRequestUpdate request);

        Task<JobListResponse>  GetJobList(JobListRequest request);


    }
}
