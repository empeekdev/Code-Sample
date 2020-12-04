using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompany.Core.Services;
using TravelCompany.WebApi.DTO;

namespace TravelCompany.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AgencyController : BaseController
    {        
        private readonly IAgencyService _agencyService;
        private readonly IAgentService _agentService;

        public AgencyController(ILogger<AgencyController> logger, IAgencyService agencyService, IAgentService agentService) 
            : base(logger)
        {            
            _agencyService = agencyService;
            _agentService = agentService;
        }

        /// <summary>
        /// Returns a list of travel agencies
        /// </summary>
        /// <returns></returns>
        [HttpGet, ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<Agency>>), 200)]
        [Route("")]
        public async Task<IActionResult> GetList()
        {
            var result = await _agencyService.GetAll();

            return ProcessResult(result, 
                a => Ok(new BaseResponse<IEnumerable<Agency>>(a.Select(x => x.ToDTOModel())))
            );
        }

        /// <summary>
        /// Add a new travel agency
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Agency>), 200)]
        [ProducesResponseType(500)]
        [Route("")]
        public IActionResult AddAgency([FromBody] AgencyCreate model)
        {
            var result = _agencyService.Add(model.ToDataModel());

            return ProcessResult(result, 
                a => Ok(new BaseResponse<Agency>(a.ToDTOModel()))
            );
        }

        /// <summary>
        /// Returns a list of agents by the travel agency UUID
        /// </summary>
        /// <param name="agencyUUID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<Agent>>), 200)]
        [ProducesResponseType(500)]
        [Route("{agencyUUID:guid}/agents")]
        public async Task<IActionResult> GetAgentsByAgencyUUID(Guid agencyUUID)
        {
            var result = await _agentService.Get(agencyUUID);

            return ProcessResult(result,
                a => Ok(new BaseResponse<IEnumerable<Agent>>(a.Select(x => x.ToDTOModel())))
            );
        }


        /// <summary>
        /// Add a new agent to a travel agency
        /// </summary>
        /// <param name="agencyUUID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<Agent>), 200)]
        [ProducesResponseType(500)]
        [Route("{agencyUUID:guid}/agents")]
        public IActionResult AddAgentByAgencyUUID(Guid agencyUUID, [FromBody] AgentCreate model)
        {
            var result = _agentService.Add(agencyUUID, model.ToDataModel());

            return ProcessResult(result,
                a => Ok(new BaseResponse<Agent>(a.ToDTOModel()))
            );
        }

        /// <summary>
        /// Bulk import agencies and their agents. Data should be accepted as a .zip archive, which contains several xml files. 
        /// Each file holds data about agents and some agency metadata.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        [Route("upload/zip")]
        public IActionResult UploadAgencies(IFormFile file)
        {            
            var result = _agencyService.BulkUploadZip(file);

            return ProcessResult(result,
                a => Ok(new BaseResponse<bool>(a))
            );            
        }
    }
}
