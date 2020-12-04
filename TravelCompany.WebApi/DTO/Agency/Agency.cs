using System;
using TravelCompany.DataAccess;

namespace TravelCompany.WebApi.DTO
{
    public class Agency: AgencyBase
    {        
        public Guid UUID { get; set; }         
    }    
}
