using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Model;
using TravelCompany.Repository;

namespace TravelCompany.Core.Services.Implementations
{
    public class TravelAgencyService : ITravelAgencyService
    {        
        private readonly IUnitOfWork _uow;

        public TravelAgencyService (DbContext dbContext)
        {            
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
                //_logger.LogError(ex, ex.Message);
                return Result.GeneralError<IEnumerable<TravelAgency>>(ex);
            }
        }
    }
}
