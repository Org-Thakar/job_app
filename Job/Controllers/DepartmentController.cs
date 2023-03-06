using Job.Application.Interfaces;
using Job.Core;
using Job.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Get all Department
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize]
        public async Task<List<Department>> GetDepartments()
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _departmentRepository.GetDepartment();
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
        /// <summary>
        /// Update existing Department
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DepartmentResponse> UpdateDepartment(Department request)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _departmentRepository.UpdateDepartment(request);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
        /// <summary>
        /// Add new Department
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateDepartment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<DepartmentResponse> CreateDepartment(NewDepartment request)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _departmentRepository.CreateDepartment(request);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
    }
}
