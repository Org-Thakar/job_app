using Dapper;
using Microsoft.Extensions.Configuration;
using Job.Core.Entities;
using Job.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Job.Application.Interfaces;

namespace Job.Infrastructure.Repositories
{
    public class JobRepository : Repository<JobResponse>, IJobRepository
    {
        internal readonly ILogger<JobRepository> _logger;
        public JobRepository(IConfiguration configuration, ILogger<JobRepository> logger)
            : base(configuration)
        {
            _logger = logger;
        }

        public async Task<JobDetailsResponse> GetJobById(int Id)
        {
            JobDetailsResponse response = new JobDetailsResponse();
            try
            {
                var query = "SELECT * FROM JOB WHERE ID=@ID";
                var department = "SELECT ID,TITLE FROM Department WHERE ID=@ID";
                var location = "SELECT ID,TITLE,CITY,STATE,COUNTRY,ZIP FROM JOBLOCATION WHERE ID=@ID";

                using (var connection = CreateConnection())
                {
                 
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", Id, DbType.Int64);
                    var response1 = (await connection.QueryAsync<JobDetailsResponse>(query,parameters)).FirstOrDefault();

                    if (response1 != null)
                    {
                        var paramDepartment = new DynamicParameters();
                        paramDepartment.Add("Id", Convert.ToInt32(response1.DepartmentId), DbType.Int64);
                        var responseDepartment = (await connection.QueryAsync<Department>(department, paramDepartment)).FirstOrDefault();


                        var paraLocation = new DynamicParameters();
                        paraLocation.Add("Id", Convert.ToInt32(response1.LocationId), DbType.Int64);
                        var responseLocation = (await connection.QueryAsync<Location>(location, paraLocation)).FirstOrDefault();


                        response.Id = response1.Id;
                        response.Title = response1.Title;
                        response.Description = response1.Description;
                        response.ClosingDate = response1.ClosingDate;
                        response.DepartmentDetails = responseDepartment;
                        response.LocationDetails = responseLocation;
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(exp.Message, exp);
                _logger.LogError(ex, "Error  " + ex.StackTrace);
                return null;
            }
        }

        public async Task<JobListResponse> GetJobList(JobListRequest request)
        {
            JobListResponse response = new JobListResponse();
            try
            {
                string jobTitle = request.JobName;
                int departmentId = Convert.ToInt32(request.DepartmentId);
                int locationId = Convert.ToInt32(request.LocationId);

                var query = "SELECT JB.ID as ID,JB.TITLE as TITLE,JL.TITLE AS LOCATION,D.TITLE AS DEPARTMENT,POSTEDDATE,CLOSINGDATE  FROM JOB jb " +
              
                   "INNER JOIN DEPARTMENT D ON JB.DEPARTMENTID= D.ID " +
                    "INNER JOIN JOBLOCATION JL ON JB.LOCATIONID= JL.ID WHERE JB.TITLE LIKE CONCAT('%',@jobTitle,'%') " +
                    " AND (@departmentId=0 OR JB.DEPARTMENTID = @departmentId) AND (@locationId =0 OR JB.LOCATIONID =@locationId )";
                //    var department = "SELECT ID,TITLE FROM Department WHERE ID=@ID";
                //    var location = "SELECT ID,TITLE,CITY,STATE,COUNTRY,ZIP FROM JOBLOCATION WHERE ID=@ID";

                using (var connection = CreateConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("jobTitle", jobTitle, DbType.String);
                    parameters.Add("locationId", locationId, DbType.Int64);
                    parameters.Add("departmentId", departmentId, DbType.Int64);

                    var response1 = (await connection.QueryAsync<JobDetailsResp>(query, parameters)).ToList();

                    response.Total = response1.Count;
                    response.Data = response1;

                   


                    //        return response;
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

        public async Task<JobResponse> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM test WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<JobResponse>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<JobResponse> CreateJob(JobRequest request)
        {
            JobResponse res = new JobResponse();
            try
            {
                var query = "INSERT INTO JOB (TITLE,DESCRIPTION,LOCATIONID,DEPARTMENTID,CLOSINGDATE) OUTPUT INSERTED.Id values (@TITLE,@DESCRIPTION,@LOCATIONID,@DEPARTMENTID,@CLOSINGDATE)";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);
                parameters.Add("@DESCRIPTION", request.Description, DbType.String);
                parameters.Add("@LOCATIONID", request.LocationId, DbType.Int32);
                parameters.Add("@DEPARTMENTID", request.DeaprtmentId, DbType.Int32);
                parameters.Add("@CLOSINGDATE", request.ClosingDate, DbType.DateTime);

                using (var connection = CreateConnection())
                {
                    var JobId=  connection.QuerySingle<int>(query, parameters);
                    res.Success = true;
                    res.Id = JobId;
                    return res;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
        public async Task<JobResponse> UpdateJob(JobRequestUpdate request)
        {
            JobResponse res = new JobResponse();
            try
            {
                var query = "UPDATE JOB set TITLE =@TITLE,DESCRIPTION=@DESCRIPTION,LOCATIONID=@LOCATIONID,DEPARTMENTID=@DEPARTMENTID,CLOSINGDATE=@CLOSINGDATE WHERE ID=@ID";
                var parameters = new DynamicParameters();
                parameters.Add("@TITLE", request.Title, DbType.String);
                parameters.Add("@DESCRIPTION", request.Description, DbType.String);
                parameters.Add("@LOCATIONID", request.LocationId, DbType.Int32);
                parameters.Add("@DEPARTMENTID", request.DeaprtmentId, DbType.Int32);
                parameters.Add("@CLOSINGDATE", request.ClosingDate, DbType.DateTime);
                parameters.Add("@ID", request.Id, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    var JobId = connection.Execute(query, parameters);
                    res.Success = true;

                    return res;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
