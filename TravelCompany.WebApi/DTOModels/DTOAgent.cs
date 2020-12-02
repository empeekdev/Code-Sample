using System;
using TravelCompany.Model;

namespace TravelCompany.WebApi.DTOModels
{
    public class DTOAgent
    {
        public long Id { get; set; }
        public Guid UUID { get; set; }
        public long TravelAgentcyId { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class DTOAgentCreate
    {
        public Guid? UUID { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static class DTOAgentExtention
    {
        /// <summary> </summary>
        public static DTOAgent ToDTOModel(this Agent item)
        {
            if (item == null) return null;

            return new DTOAgent
            {
                Id = item.Id,
                UUID = item.UUID,
                FirstName = item.FirstName,
                LastName = item.LastName,
                TravelAgentcyId = item.TravelAgency.Id
            };
        }

        /// <summary> </summary>
        public static Agent ToDataModel(this DTOAgentCreate item)
        {
            if (item == null) return null;

            return new Agent
            {                
                UUID = item.UUID ?? Guid.NewGuid(),
                FirstName = item.FirstName,
                LastName = item.LastName                
            };
        }
    }
}
