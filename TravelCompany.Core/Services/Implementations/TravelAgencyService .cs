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

        public Result<TravelAgency> AddAgency(TravelAgency travelAgency)
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
    }
}
