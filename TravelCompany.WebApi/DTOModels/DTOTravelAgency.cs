using System;
using TravelCompany.Model;

namespace TravelCompany.WebApi.DTOModels
{
    public class DTOTravelAgency
    {
        public long Id { get; set; }
        public Guid UUID { get; set; } 
        public string Name { get; set; }
    }

    public class DTOTravelAgencyCreate
    {        
        public string Name { get; set; }
        public Guid? UUID { get; set; }
    }

    public static class DTOTravelAgencyExtention
    {
        /// <summary> </summary>
        public static DTOTravelAgency ToDTOModel(this TravelAgency item)
        {
            if (item == null) return null;

            return new DTOTravelAgency
            {
                Id = item.Id,
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
