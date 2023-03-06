using Job.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Job.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private IConfiguration _config;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        #region AuthenticateUser  
        /// <summary>  
        /// Hardcoded the User authentication  
        /// </summary>  
        /// <param name="login"></param>  
        /// <returns></returns>  
        private async Task<LoginModel> AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;

            //Validate the User Credentials      
            //Demo Purpose, I have Passed HardCoded User Information      
            if (login.UserName == "Jay")
            {
                user = new LoginModel { UserName = "Jay", Password = "123456" };
            }
            return user;
        }
        #endregion

        #region Login Validation  
        /// <summary>  
        /// Login Authenticaton using JWT Token Authentication  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            if (data != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }
        #endregion

        #region Get  
        /// <summary>  
        /// Authorize the Method  
        /// </summary>  
        /// <returns></returns>  
        [HttpGet(nameof(Get))]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }
        #endregion  
    }
}
