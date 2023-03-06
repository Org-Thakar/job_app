using Job.Core;
using Job.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartment();
        Task<DepartmentResponse> UpdateDepartment(Department request);
        Task<DepartmentResponse> CreateDepartment(NewDepartment request);
    }
}
