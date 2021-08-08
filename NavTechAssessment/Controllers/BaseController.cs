using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NavTechAssesment.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NavTechAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Handles the exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>throws exception</returns>
        protected async Task<IActionResult> HandleExceptions(Exception ex)
        {
            if(ex is ConfigEntityException)
            {
                var exception = (ConfigEntityException)ex;
                return StatusCode((int)exception.StatusCode, exception.Message);
            }
            return StatusCode(500, "Something went wrong, we are looking in to it");
        }
    }
}
