﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompany.DataAccess;

namespace TravelCompany.Core.Services
{
    public interface IAgentService
    {
        Task<Result<IEnumerable<Agent>>> Get(Guid agencyUUID);
        Result<Agent> Add(Guid uuid, Agent agent);
    }
}
