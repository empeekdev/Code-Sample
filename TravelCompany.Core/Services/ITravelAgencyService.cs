using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Model;

namespace TravelCompany.Core.Services
{
    public interface ITravelAgencyService
    {
        Task<Result<IEnumerable<TravelAgency>>> GetAllTravelAgencies();
        Task<Result<IEnumerable<Agent>>> GetAgents(Guid travelAgencyUUID);
        Result<TravelAgency> AddTravelAgency(TravelAgency travelAgency);
        Result<Agent> AddAgent(Guid uuid, Agent agent);
        Result<bool> BulkUploadZip(IFormFile file);
        
    }
}
