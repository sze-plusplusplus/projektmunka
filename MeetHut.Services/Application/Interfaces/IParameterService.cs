using MeetHut.Services.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Application.Interfaces
{
    /// <summary>
    /// Parameter Service
    /// </summary>
    public interface IParameterService
    {
        /// <summary>
        /// Get by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Parameter</returns>
        ParameterDTO Get(string key);

        /// <summary>
        /// Get all parameter
        /// </summary>
        /// <returns>All parameters</returns>
        List<ParameterDTO> GetAll();
    }
}
