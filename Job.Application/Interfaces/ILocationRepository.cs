using Job.Core.Entities;
using Job.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Application.Interfaces
{
    public interface ILocationRepository:IRepository<Location>
    {
        Task<List<Location>> GetLocations();
        Task<LocationResponse> UpdateLocation(LocationRequest request);
        Task<LocationResponse> CreateLocation(LocationRequest request);
    }
}
