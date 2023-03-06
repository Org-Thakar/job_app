using System.Data;
using Job.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Job.Infrastructure.Repositories.Base;
using Job.Core.Entities;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Job.Infrastructure.Repositories
{
    public class BaseRepository : Repository<JobResponse>, IBaseRepository
    {
        internal readonly ILogger<BaseRepository> _logger;
        private readonly IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
            : base(configuration)
        {
          
            _configuration = configuration;
        }

        public  bool ValidateLicenseKey()
        {
            string License = _configuration.GetValue<string>("LicenseKey");
           
            return true;

        }


       
        public string EncryptString(string key, string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
            return System.Convert.ToBase64String(plainTextBytes);

        }
        public string DecryptString(string key, string plainText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(key);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }
        public string EncryptString1(string key, string plainText)
        {


            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = plainText;

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(key);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

        }

        public string DecryptString1(string strEncrypted, string strKey)
        {
            //var base64EncodedBytes = System.Convert.FromBase64String(key);
            //return System.Text.Encoding.UTF32.GetString(base64EncodedBytes);

            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;
            string strTempKey = strKey;

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(strEncrypted);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;

            return strDecrypted;

        }



    }
}
