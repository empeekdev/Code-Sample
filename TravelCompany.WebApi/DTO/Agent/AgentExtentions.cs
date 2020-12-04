using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelCompany.WebApi.DTO
{
    public static class AgentExtentions
    {
        public static Agent ToDTOModel(this DataAccess.Agent item)
        {
            if (item == null) return null;

            return new Agent
            {
                UUID = item.UUID,
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }

        public static DataAccess.Agent ToDataModel(this AgentCreate item)
        {
            if (item == null) return null;

            return new DataAccess.Agent
            {
                UUID = item.UUID ?? Guid.NewGuid(),
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }
    }
}
