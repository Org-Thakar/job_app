using Dapper;
using Microsoft.Extensions.Configuration;
using Job.Core.Entities;
using Job.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Job.Application.Interfaces;

namespace Job.Infrastructure.Repositories
{
    public class LocationRepository: Repository<LocationResponse>, ILocationRepository
    {
        public readonly ILogger<LocationRepository> _logger;
        public LocationRepository(IConfiguration configuration, ILogger<LocationRepository> logger)
            : base(configuration)
        {
            _logger = logger;
        }

        public async Task<List<Location>> GetLocations()
        {
            List<Location> response = new List<Location>();
            try
            {
                var query = "SELECT Id,Title,city,state,country,zip FROM JobLocation ";


                using (var connection = CreateConnection())
                {

                    response = connection.Query<Location>(query).ToList();
                   
                }
                return response;
            }
            catch (Exception ex)
            {
                //throw new Exception(exp.Message, exp);
                _logger.LogError(ex, "Error  " + ex.StackTrace);
                return null;
            }
        }


        public async Task<LocationResponse> CreateLocation(LocationRequest request)
        {
            LocationResponse response = new LocationResponse();
            try
            {
                var query = "INSERT INTO JOBLOCATION (TITLE,City,State,Country,Zip) OUTPUT INSERTED.Id values (@TITLE,@City,@State,@Country,@Zip)";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);
               
                parameters.Add("@City", request.City, DbType.String);
                parameters.Add("@State", request.State, DbType.String);
                parameters.Add("@Country", request.Country, DbType.String);
                parameters.Add("@Zip", request.Zip, DbType.String);

                using (var connection = CreateConnection())
                {
                    var LocationId = connection.QuerySingle<int>(query, parameters);
                    //res.Success = true;
                    response.Id = LocationId;
                    response.IsSuccess = true;
                    return response;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
        public async Task<LocationResponse> UpdateLocation(LocationRequest request)
        {
            LocationResponse response = new LocationResponse();
            try
            {
                var query = "UPDATE JOBLOCATION set TITLE =@TITLE,City=@City,State=@State,Country=@Country,Zip=@Zip WHERE ID=@ID";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);
               
                parameters.Add("@City", request.City, DbType.String);
                parameters.Add("@State", request.State, DbType.String);
                parameters.Add("@Country", request.Country, DbType.String);
                parameters.Add("@Zip", request.Zip, DbType.String);
                parameters.Add("@ID", request.Id, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    var location = connection.Execute(query, parameters);
                    response.IsSuccess = true;

                    return response;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
