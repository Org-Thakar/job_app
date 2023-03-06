using System;
using System.Collections.Generic;
using System.Text;

namespace Job.Core.Entities.Base
{
    public class BaseResponse
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public Int64 Id { get; set; }
        //public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; private set; }
        

        public BaseResponse()
        {
            this.ModifiedDate = DateTime.Now;
        }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
       
    }
}
