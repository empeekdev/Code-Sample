using System;
using System.Collections.Generic;
using System.Text;
using TravelCompany.Model;

namespace TravelCompany.Core.Validation
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

            if (model.TravelAgency == null)
                errors.Add(ValidationErrorCodes.Agent.TravelAgencyIsNotFound());

            return errors;
        }
    }
}
