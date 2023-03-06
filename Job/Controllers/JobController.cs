
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Job.Application.Models;
using Job.Application.Interfaces;

using System;

using System.Threading.Tasks;
using Job.Core.Entities;
//using Job.Core.Repositories;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
     
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        /// <summary>
        /// Search Job by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetJobById")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<Core.Entities.JobDetailsResponse> GetJobById(int Id)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
           var resp= await _jobRepository.GetJobById(Id);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
        /// <summary>
        /// List of all Jobs
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetJobList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JobListResponse> GetJobList([FromQuery] JobListRequest request)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var response = await _jobRepository.GetJobList(request);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return response;
        }
        /// <summary>
        /// Add new Job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateJob")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Core.Entities.JobResponse> CreateJob(JobRequest request)
        {
            return await _jobRepository.CreateJob(request);
        }
        /// <summary>
        /// Update existing Job
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Core.Entities.JobResponse> UpdateJob(JobRequestUpdate request)
        {
            return await _jobRepository.UpdateJob(request);
        }
    }
}
