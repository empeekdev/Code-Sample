using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompany.Core.Enums;
using TravelCompany.Core.Models;

namespace TravelCompany.WebApi.Controllers
{
    public class BaseController: ControllerBase
    {
        protected ILogger Logger { get; }

        protected BaseController(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected IActionResult ProcessResult<T>(Result<T> result, Func<T, IActionResult> OkAction = null)
        {
            bool isProduction = false;//get from Config
            switch (result?.Status)
            {
                case ResultStatusEnum.Success:
                    if (OkAction != null)
                        return OkAction(result.Obj);
                    return Ok("OK");
                case ResultStatusEnum.ValidationError:
                    return BadRequest(JsonConvert.SerializeObject(result.Errors));
                case ResultStatusEnum.GeneralError:
                    if (isProduction)
                        return NotFound();
                    return Problem(result.Message);
                case ResultStatusEnum.NotFound:
                case null:
                    if (result?.Errors?.Count > 0)
                        return NotFound(JsonConvert.SerializeObject(result.Errors));
                    return NotFound();
                case ResultStatusEnum.NotSupported:
                default:
                    return base.Conflict();
            }
        }
    }
}
