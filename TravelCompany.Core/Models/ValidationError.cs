using System;
using System.Collections.Generic;
using System.Text;

namespace TravelCompany.Core.Models
{
    public class ValidationError
    {
        /// <summary>
        /// Code of the error
        /// </summary>
        public long Code { get; set; }
        /// <summary>
        /// Message of the error
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Field that has error
        /// </summary>
        public string Field { get; set; }

        public ValidationError(long code, string message, string field = null)
        {
            Code = code;
            Message = message;
            Field = field;
        }
    }
}
