using Microsoft.AspNetCore.Mvc;
using Job.Core.Entities;
using Job.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetLocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Location>> GetLocations()
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _locationRepository.GetLocations();
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
        /// <summary>
        /// Update existing Location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateLocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<LocationResponse> UpdateLocation(LocationRequest request)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _locationRepository.UpdateLocation(request);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
        /// <summary>
        /// Add new Location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateLocation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<LocationResponse> CreateLocation(LocationRequest request)
        {

            //  return Core.Entities.JobResponse await _jobRepository.GetJobById(Id);
            var resp = await _locationRepository.CreateLocation(request);
            // return await _mediator.Send((IRequest<List<Core.Entities.Customer>>)new Application.Models.CustomerModel());
            return resp;
        }
    }
}
