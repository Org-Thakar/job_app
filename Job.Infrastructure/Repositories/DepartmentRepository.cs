using Microsoft.Extensions.Configuration;
using Job.Application.Interfaces;
using Job.Core;
using Job.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Core.Entities;
using Dapper;
using System.Linq;
using System.Data;

namespace Job.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<DepartmentResponse>, IDepartmentRepository
    {
        public readonly ILogger<LocationRepository> _logger;
        public DepartmentRepository(IConfiguration configuration, ILogger<LocationRepository> logger)
            : base(configuration)
        {
            _logger = logger;
        }

        public async Task<List<Department>> GetDepartment()
        {
            List<Department> response = new List<Department>();
            try
            {
                var query = "SELECT Id,Title FROM Department";


                using (var connection = CreateConnection())
                {

                    response = connection.Query<Department>(query).ToList();

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


        public async Task<DepartmentResponse> CreateDepartment(NewDepartment request)
        {
            DepartmentResponse response = new DepartmentResponse();
            try
            {
                var query = "INSERT INTO Department (TITLE) OUTPUT INSERTED.Id values (@TITLE)";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);

                

                using (var connection = CreateConnection())
                {
                    var departmentId = connection.QuerySingle<int>(query, parameters);
                    //res.Success = true;
                    response.IsSuccess = true;
                    response.Id = departmentId;
                    return response;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
        public async Task<DepartmentResponse> UpdateDepartment(Department request)
        {
            DepartmentResponse response = new DepartmentResponse();
            try
            {
                var query = "UPDATE Department set TITLE =@TITLE WHERE ID=@ID";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);
                parameters.Add("@ID", request.Id, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    var Department = connection.Execute(query, parameters);
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
