using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetHut.Services.Application
{
    /// <inheritdoc />
    public class ParameterService : IParameterService
    {
        private static readonly Dictionary<string, string> keyPairs = new Dictionary<string, string>
        {
            { "Microsoft.ClientId", "ExternalAuthentication:Microsoft:ClientId" },
            { "Google.ClientId", "ExternalAuthentication:Google:ClientId" },
            { "Microsoft.Login", "ExternalAuthentication:Microsoft:LoginDisabled" },
            { "Google.Login", "ExternalAuthentication:Google:LoginDisabled" },
            { "Registration", "DisableRegistration" }
        };
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Init Parameter Service
        /// </summary>
        /// <param name="configuration">App Configuration</param>
        public ParameterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public ParameterDTO Get(string key)
        {
            var configKey = keyPairs[key];
            var configValue = _configuration[configKey];

            if (string.IsNullOrEmpty(configValue))
            {
                return null;
            }

            return new ParameterDTO { Key = key, Value = configValue };
        }

        /// <inheritdoc />
        public List<ParameterDTO> GetAll()
        {
            return keyPairs
                .Select(key => new ParameterDTO { Key = key.Key, Value = _configuration[key.Value] })
                .ToList();
        }
    }
}
