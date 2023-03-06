using System;
using Job.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Job.Infrastructure.Repositories;

namespace Job.Infrastructure.Filters
{
    public class LicenseFilterAttribute : Attribute, IActionFilter
    {
        private  IBaseRepository _baseRepository;
        public IBaseRepository MyProperty { get; set; }
        public LicenseFilterAttribute()
        {
            // _baseRepository = IBaseRepository;
           
        }
        public  void OnActionExecuting(ActionExecutingContext context)
        {
           
            _baseRepository = (IBaseRepository)context.HttpContext.RequestServices.GetService(typeof(IBaseRepository));
            bool valid = _baseRepository.ValidateLicenseKey();
            if (!valid)
            {
                throw new Exception("Invalid License Key");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To do : after the action executes  
        }
    }
}
