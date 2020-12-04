using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelCompany.DataAccess
{
    public enum ResultStatusEnum
    {
        Success,
        GeneralError,
        ValidationError,
        [Description("Object not found")]
        NotFound,
        [Description("Bad request")]
        BadRequest,
        [Description("Operation is not supported")]
        NotSupported
    }
}
