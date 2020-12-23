using ELRDServerAPI.Contracts.V1;
using ELRDServerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ELRDServerAPI.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BaseDataController : Controller
    {

        private readonly IBaseDataService _basedataService;
        private readonly ILogger<UserService> _logger;

        public BaseDataController(IBaseDataService basedataService, ILogger<UserService> log)
        {
            _basedataService = basedataService;
            _logger = log;
        }

        [HttpGet(ApiRoutes.BaseData.BaseUnit)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Base Unit GET Request");
            return Ok(await _basedataService.GetBaseUnitAsync());
        }

        [HttpGet(ApiRoutes.BaseData.SeedBaseUnit)]
        public async Task<IActionResult> SeedData()
        {
            _logger.LogInformation("Seed Base Units");
            try
            {
                await _basedataService.SeedBaseUnits();
                _logger.LogInformation(String.Format("Added Base Units to db"));
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(String.Format("Can't seed base unit data to db {0}", ex.Message));
            }
            return Ok();
        }
    }
}
