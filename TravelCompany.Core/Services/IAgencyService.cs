﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.Core.Models;
using TravelCompany.DataAccess;

namespace TravelCompany.Core.Services
{
    public interface IAgencyService
    {
        Task<Result<IEnumerable<Agency>>> GetAll();        
        Result<Agency> Add(Agency agency);        
        Result<bool> BulkUploadZip(IFormFile file);        
    }
}
