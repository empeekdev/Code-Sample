using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Core.Validation;
using TravelCompany.DataAccess;
using TravelCompany.Repository;

namespace TravelCompany.Core.Services.Implementations
{
    public class AgentService: IAgentService
    {
        private readonly ILogger<AgentService> _logger;
        private readonly IUnitOfWork _uow;

        public AgentService(ILogger<AgentService> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }
        
        public async Task<Result<IEnumerable<Agent>>> Get(Guid agencyUUID)
        {
            try
            {
                var agents = await _uow.AgentRepository.Select().Where(x => x.Agency.UUID == agencyUUID).ToListAsync();
                return Result.Success(agents.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<IEnumerable<Agent>>(ex);
            }
        }

        public Result<Agent> Add(Guid uuid, Agent agent)
        {
            try
            {
                var agency = _uow.AgencyRepository.GetByUUID(uuid);
                agent.Agency = agency;

                var validationErrors = agent.Validate();
                if (validationErrors.Any())
                    return Result.ValidationError<Agent>(validationErrors);

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
