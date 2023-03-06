using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.Interfaces
{
    public interface IBaseRepository
    {
        bool ValidateLicenseKey();
    }
}
