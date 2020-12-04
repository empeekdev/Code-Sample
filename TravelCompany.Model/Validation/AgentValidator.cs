using System.Collections.Generic;

namespace TravelCompany.DataAccess
{
    public static class AgentValidator
    {
        public static List<ValidationError> Validate(this Agent model)
        {
            var errors = new List<ValidationError>();

            if (model == null)
            {
                errors.Add(ValidationErrorCodes.Common.ModelIsEmpty());
                return errors;
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
                errors.Add(ValidationErrorCodes.Agent.FirstNameCantBeEmpty());

            if (string.IsNullOrWhiteSpace(model.LastName))
                errors.Add(ValidationErrorCodes.Agent.LastNameCantBeEmpty());

            if (model.Agency == null)
                errors.Add(ValidationErrorCodes.Agent.AgencyIsNotFound());

            return errors;
        }
    }
}
