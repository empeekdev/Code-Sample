using TravelCompany.Model;

namespace TravelCompany.WebApi.DTOModels
{
    public class DTOTravelAgency
    {
        public long Id { get; set; }
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
                Id = item.Id,
                Name = item.Name
            };
        }

        /// <summary> </summary>
        public static TravelAgency ToDataModel(this DTOTravelAgency item)
        {
            if (item == null) return null;

            return new TravelAgency
            {                
                Name = item.Name
            };
        }
    }
}
