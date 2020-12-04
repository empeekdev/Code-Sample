using System;

namespace TravelCompany.WebApi.DTO
{
    public static class AgencyExtentions
    {
        public static Agency ToDTOModel(this DataAccess.Agency item)
        {
            if (item == null) return null;

            return new Agency
            {
                Name = item.Name,
                UUID = item.UUID
            };
        }

        public static DataAccess.Agency ToDataModel(this AgencyCreate item)
        {
            if (item == null) return null;

            return new DataAccess.Agency
            {
                Name = item.Name,
                UUID = item.UUID ?? Guid.NewGuid()
            };
        }
    }
}
