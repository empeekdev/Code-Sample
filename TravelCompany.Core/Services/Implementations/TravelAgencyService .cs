﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.Core.Validation;
using TravelCompany.Model;
using TravelCompany.Repository;
using System.Linq;
using System.IO.Compression;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Text;

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

        public async Task<Result<IEnumerable<Agent>>> GetAgents(Guid travelAgencyUUID)
        {
            try
            {
                var agents = await _uow.AgentRepository.Select().Where(x => x.TravelAgency.UUID == travelAgencyUUID).ToListAsync();
                return Result.Success(agents.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<IEnumerable<Agent>>(ex);
            }
        }       

        public Result<Agent> AddAgent(Guid uuid, Agent agent)
        {
            try
            {
                var travelAgency = _uow.TravelAgencyRepository.GetByUUID(uuid);
                agent.TravelAgency = travelAgency;

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
       
        public Result<bool> BulkUploadZip(IFormFile file)
        {
            try
            {                
                var serializer = new XmlSerializer(typeof(TravelAgency));
                var travelAgencies = new List<TravelAgency>();
                var agents = new List<Agent>();

                using (var stream = file.OpenReadStream())
                using (var archive = new ZipArchive(stream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        var travelAgency = serializer.Deserialize(entry.Open()) as TravelAgency;

                        var validationErrors = travelAgency.Validate();
                        if (validationErrors.Any())
                            return Result.ValidationError<bool>(validationErrors);
                        
                        travelAgencies.Add(travelAgency);
                    }
                }

                _uow.BeginTransaction();
                
                foreach (var travelAgency in travelAgencies)
                {
                    var dbTravelAgency = _uow.TravelAgencyRepository.GetByUUID(travelAgency.UUID);
                    if (dbTravelAgency != null)
                    {                        
                        _uow.TravelAgencyRepository.Delete(dbTravelAgency);
                        _uow.SaveChanges();
                    }
                }

                _uow.TravelAgencyRepository.Add(travelAgencies);
                _uow.SaveChanges();

                _uow.Commit();

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<bool>(ex);
            }
        }              

    }
}
