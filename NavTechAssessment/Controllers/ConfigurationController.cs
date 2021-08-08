using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavTechAssesment.Domain.Services.Interfaces;
using NavTechAssessment.Models.Request;
using NavTechAssessment.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NavTechAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : BaseController
    {
        private readonly INavTechConfigurationService _navTechConfigurationService;
        public ConfigurationController(INavTechConfigurationService navTechConfigurationService)
        {
            _navTechConfigurationService = navTechConfigurationService;
        }

        [HttpGet("{entity}")]
        public async Task<IActionResult> GetConfiguration([FromRoute] string entity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity))
                {
                    return BadRequest("entity cannot be null or empty");
                }
                var result = await _navTechConfigurationService.GetConfiguration(entity).ConfigureAwait(false);
                return Ok(new ConfigurationResponseModel(result));
            }
            catch (Exception ex)
            {
                return await HandleExceptions(ex).ConfigureAwait(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostConfiguration([FromBody] ConfigurationRequestModel configurationRequestModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _navTechConfigurationService.AddNewConfiguration(configurationRequestModel.ToDto()).ConfigureAwait(false);

                return Ok(new ConfigurationResponseModel(result));
            }
            catch (Exception ex)
            {
                return await HandleExceptions(ex).ConfigureAwait(false);
            }
        }

        [HttpDelete("{entity}")]
        public async Task<IActionResult> DeleteConfiguration([FromRoute] string entity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity))
                {
                    return BadRequest("entity cannot be null or empty");
                }
                var IsDeleted = await _navTechConfigurationService.DeleteConfiguration(entity).ConfigureAwait(false);
                return IsDeleted ? NoContent() : StatusCode((int)HttpStatusCode.BadRequest, "something bad happend");
            }
            catch (Exception ex)
            {
                return await HandleExceptions(ex).ConfigureAwait(false);
            }
        }

        [HttpDelete("{entity}/{field}")]
        public async Task<IActionResult> DeleteMetadata([FromRoute] string entity, [FromRoute] string field)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity) || string.IsNullOrWhiteSpace(field))
                {
                    return BadRequest("entity or field cannot be null or empty");
                }
                var IsDeleted = await _navTechConfigurationService.DeleteEntityMetadata(entity, field).ConfigureAwait(false);
                return IsDeleted ? NoContent() : StatusCode((int)HttpStatusCode.BadRequest, "something bad happend");
            }
            catch(Exception ex)
            {
                return await HandleExceptions(ex).ConfigureAwait(false);
            }
        }

    }
}
