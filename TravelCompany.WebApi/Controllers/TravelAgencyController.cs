using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TravelCompany.Core.Services;
using TravelCompany.WebApi.DTOModels;

namespace TravelCompany.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TravelAgencyController : BaseController
    {        
        private readonly ITravelAgencyService _testService;
        public TravelAgencyController(ILogger<TravelAgencyController> logger, ITravelAgencyService testService): base(logger)
        {            
            _testService = testService;
        }

        /// <summary>
        /// Returns a list of travel agencies
        /// </summary>
        /// <returns></returns>
        [HttpGet, ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<DTOTravelAgency>>), 200)]
        [Route("")]
        public async Task<IActionResult> GetList()
        {
            var result = await _testService.GetAllTravelAgencies();

            return ProcessResult(result, 
                a => Ok(new BaseResponse<IEnumerable<DTOTravelAgency>>(a.Select(x => x.ToDTOModel())))
            );
        }

        /// <summary>
        /// Add a new travel agency
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<DTOTravelAgency>), 200)]
        [ProducesResponseType(500)]
        [Route("")]
        public IActionResult AddTravelAgency([FromBody] DTOTravelAgencyCreate model)
        {
            var result = _testService.AddTravelAgency(model.ToDataModel());

            return ProcessResult(result, 
                a => Ok(new BaseResponse<DTOTravelAgency>(a.ToDTOModel()))
            );
        }

        /// <summary>
        /// Returns a list of agents by the travel agency UUID
        /// </summary>
        /// <param name="travelAgencyUUID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<DTOAgent>>), 200)]
        [ProducesResponseType(500)]
        [Route("{travelAgencyUUID:guid}/agents")]
        public async Task<IActionResult> GetAgentsByTravelAgencyUUID(Guid travelAgencyUUID)
        {
            var result = await _testService.GetAgents(travelAgencyUUID);

            return ProcessResult(result,
                a => Ok(new BaseResponse<IEnumerable<DTOAgent>>(a.Select(x => x.ToDTOModel())))
            );
        }


        /// <summary>
        /// Add a new agent to a travel agency
        /// </summary>
        /// <param name="travelAgencyUUID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<DTOAgent>), 200)]
        [ProducesResponseType(500)]
        [Route("{travelAgencyUUID:guid}/agents")]
        public IActionResult AddAgentByTravelAgencyUUID(Guid travelAgencyUUID, [FromBody] DTOAgentCreate model)
        {
            var result = _testService.AddAgent(travelAgencyUUID, model.ToDataModel());

            return ProcessResult(result,
                a => Ok(new BaseResponse<DTOAgent>(a.ToDTOModel()))
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
        public IActionResult UploadTravelAgencies(IFormFile file)
        {            

            var result = _testService.BulkUploadZip(file);

            return ProcessResult(result,
                a => Ok(new BaseResponse<bool>(a))
            );            
        }
    }
}
