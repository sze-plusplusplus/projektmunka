using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Parameter controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterService _parameterService;

        /// <summary>
        /// Init Parameter controller
        /// </summary>
        /// <param name="parameterService">Parameter Service</param>
        public ParameterController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        /// <summary>
        /// Get by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Parameter</returns>
        [HttpGet("{key}")]
        public ParameterDTO Get(string key) 
        {
            return _parameterService.Get(key);
        }

        /// <summary>
        /// Get all parameter
        /// </summary>
        /// <returns>Parameters</returns>
        [HttpGet]
        public List<ParameterDTO> GetAll ()
        {
            return _parameterService.GetAll();
        }
    }
}
