using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Core.Validation;
using TravelCompany.Model;
using TravelCompany.Repository;
using System.Linq;

namespace TravelCompany.Core.Services.Implementations
{
    public class TravelAgencyService : ITravelAgencyService
    {
        private readonly ILogger<TravelAgencyService> _logger;
        private readonly IUnitOfWork _uow;

        public TravelAgencyService (ILogger<TravelAgencyService> logger, DbContext dbContext)
        {
            _logger = logger;
            _uow = new UnitOfWork(dbContext);            
        }

        public async Task<Result<IEnumerable<TravelAgency>>> GetAllTravelAgencies()
        {
            try
            {                
                var travelAgencies = await _uow.TravelAgencyRepository.GetAllAsync();
                return Result.Success(travelAgencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<IEnumerable<TravelAgency>>(ex);
            }
        }

        public Result<TravelAgency> AddTravelAgency(TravelAgency travelAgency)
        {
            try
            {
                var validationErrors = travelAgency.Validate();
                if (validationErrors.Any())
                    return Result.ValidationError<TravelAgency>(validationErrors);

                var item = _uow.TravelAgencyRepository.Add(travelAgency);
                _uow.SaveChanges();

                return Result.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<TravelAgency>(ex);
            }
        }

        public Result<Agent> AddAgent(long travelAgencyId, Agent agent)
        {
            try
            {
                //var validationErrors = agent.Validate();
                //if (validationErrors.Any())
                //    return Result.ValidationError<TravelAgency>(validationErrors);

                var travelAgency = _uow.TravelAgencyRepository.GetById(travelAgencyId);

                agent.TravelAgency = travelAgency;

                var item = _uow.AgentRepository.Add(agent);
                _uow.SaveChanges();

                return Result.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<Agent>(ex);
            }
        }
    }
}
