using Job.Application.Interfaces;
using Job.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;



using Microsoft.Extensions.Logging;

namespace Job.API.Controllers.Base
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {

        internal readonly ILogger<BaseRepository> _logger;
        private readonly IBaseRepository _baseRepository;
        public BaseController(IBaseRepository baseRepository, ILogger<BaseRepository> logger)
        {
            _logger = logger;
            _baseRepository = baseRepository;
            //bool valid = await _baseRepository.ValidateLicenseKey();
            //ValidateLicenseKey();
        }
       
        //public void ValidateLicenseKey()
        //{
        //    //bool valid= _baseRepository.ValidateLicenseKey();
        //}
    }
}
