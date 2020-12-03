using System;
using TravelCompany.Model;

namespace TravelCompany.WebApi.DTOModels
{
    public class DTOTravelAgency
    {        
        public Guid UUID { get; set; } 
        public string Name { get; set; }
    }

    public class DTOTravelAgencyCreate
    {
        public Guid? UUID { get; set; }
        public string Name { get; set; }        
    }

    public static class DTOTravelAgencyExtention
    {
        /// <summary> </summary>
        public static DTOTravelAgency ToDTOModel(this TravelAgency item)
        {
            if (item == null) return null;

            return new DTOTravelAgency
            {                
                Name = item.Name,
                UUID = item.UUID
            };
        }

        /// <summary> </summary>
        public static TravelAgency ToDataModel(this DTOTravelAgencyCreate item)
        {
            if (item == null) return null;

            return new TravelAgency
            {                
                Name = item.Name,
                UUID = item.UUID ?? Guid.NewGuid()
            };
        }
    }
}
