using System;
using System.Collections.Generic;
using System.Text;

namespace TravelCompany.Core.Validation
{
    public static class ValidationErrorCodes
    {
        /// <summary>
        /// Defines Common errors
        /// Error codes:  1000...1999    
        /// </summary>
        public static class Common
        {
            public static ValidationError IncorrectGuid(string id) => new ValidationError(1000, $"Provided id is not GUID: {id}");
            public static ValidationError ModelIsEmpty() => new ValidationError(1010, $"Provided model is empty");
        }

        /// <summary>
        ///  Defines errors for TravelAgency 
        /// Error codes:  2000...2999    
        /// </summary>
        public static class TravelAgency
        {           
            public static ValidationError NameCantBeEmpty() => new ValidationError(2000, $"Name of the travel agency can't be empty.", "Name");
        }
    }
}
