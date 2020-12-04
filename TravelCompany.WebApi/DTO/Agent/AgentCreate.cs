using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelCompany.WebApi.DTO
{
    public class AgentCreate: AgentBase
    {
        public Guid? UUID { get; set; }
    }
}
