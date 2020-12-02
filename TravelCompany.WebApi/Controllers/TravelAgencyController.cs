using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            var result = await _testService.GetAllTravelAgencies();

            return ProcessResult(result, 
                a => Ok(new BaseResponse<IEnumerable<DTOTravelAgency>>(a.Select(x => x.ToDTOModel())))
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<DTOTravelAgency>), 200)]
        [ProducesResponseType(500)]
        [Route("add")]
        public IActionResult PostCreate([FromBody] DTOTravelAgencyCreate model)
        {
            var result = _testService.AddAgency(model.ToDataModel());

            return ProcessResult(result, 
                a => Ok(new BaseResponse<DTOTravelAgency>(a.ToDTOModel()))
            );
        }
    }
}
