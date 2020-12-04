using System.Collections.Generic;
using TravelCompany.DataAccess;

namespace TravelCompany.DataAccess
{
    public static class AgencyValidator
    {
        public static List<ValidationError> Validate(this Agency model)
        {
            var errors = new List<ValidationError>();

            if (model == null)
            {
                errors.Add(ValidationErrorCodes.Common.ModelIsEmpty());
                return errors;
            }

            if (string.IsNullOrWhiteSpace(model.Name))
                errors.Add(ValidationErrorCodes.Agency.NameCantBeEmpty());

            return errors;
        }
    }
}
