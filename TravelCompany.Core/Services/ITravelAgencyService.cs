using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Model;

namespace TravelCompany.Core.Services
{
    public interface ITravelAgencyService
    {
        Task<Result<IEnumerable<TravelAgency>>> GetAllTravelAgencies();
        Result<TravelAgency> AddTravelAgency(TravelAgency travelAgency);
        Result<Agent> AddAgent(long travelAgencyId,  Agent agent);
    }
}
