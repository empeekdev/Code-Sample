using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelCompany.Core.Models;
using TravelCompany.Core.Validation;
using TravelCompany.DataAccess;
using TravelCompany.Repository;

namespace TravelCompany.Core.Services.Implementations
{
    public class AgencyService : IAgencyService
    {
        private readonly ILogger<AgencyService> _logger;
        private readonly IUnitOfWork _uow;

        public AgencyService (ILogger<AgencyService> logger, IUnitOfWork uow)
        {
            _logger = logger;            
            _uow = uow;
        }

        public async Task<Result<IEnumerable<Agency>>> GetAll()
        {
            try
            {
                var agencies = await _uow.AgencyRepository.GetAllAsync();
                return Result.Success(agencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<IEnumerable<Agency>>(ex);
            }
        }
        
        public Result<Agency> Add(Agency agency)
        {
            try
            {
                var validationErrors = agency.Validate();
                if (validationErrors.Any())
                    return Result.ValidationError<Agency>(validationErrors);

                var item = _uow.AgencyRepository.Add(agency);
                _uow.SaveChanges();

                return Result.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result.GeneralError<Agency>(ex);
            }
        }        
       
        public Result<bool> BulkUploadZip(IFormFile file)
        {
            try
            {                
                var serializer = new XmlSerializer(typeof(Agency));
                var agencies = new List<Agency>();
                var agents = new List<Agent>();

                using (var stream = file.OpenReadStream())
                using (var archive = new ZipArchive(stream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        var agency = serializer.Deserialize(entry.Open()) as Agency;

                        var validationErrors = agency.Validate();
                        if (validationErrors.Any())
                            return Result.ValidationError<bool>(validationErrors);
                        
                        agencies.Add(agency);
                    }
                }

                using (_uow.BeginTransaction())
                {
                    foreach (var agency in agencies)
                    {
                        var dbAgency = _uow.AgencyRepository.GetByUUID(agency.UUID);
                        if (dbAgency != null)
                        {
                            _uow.AgencyRepository.Delete(dbAgency);
                            _uow.SaveChanges();
                        }
                    }

                    _uow.AgencyRepository.Add(agencies);
                    _uow.SaveChanges();

                    _uow.Commit();
                }

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
