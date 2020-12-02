using System;
using System.Collections.Generic;
using System.Text;
using TravelCompany.Model;

namespace TravelCompany.Core.Validation
{
    public static class TravelAgencyValidator
    {
        public static List<ValidationError> Validate(this TravelAgency model)
        {
            var errors = new List<ValidationError>();

            if (model == null)
            {
                errors.Add(ValidationErrorCodes.Common.ModelIsEmpty());
                return errors;
            }

            if (string.IsNullOrWhiteSpace(model.Name))
                errors.Add(ValidationErrorCodes.TravelAgency.NameCantBeEmpty());

            return errors;
        }
    }
}
