﻿using MeetHut.Services.Application.DTOs;
using MeetHut.Services.Application.Interfaces;
using MeetHut.Services.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Token Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Init Token controller
        /// </summary>
        public TokenController(ITokenService tokenService) 
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Refreshed token</returns>
        [HttpPost("refresh")]
        public TokenDTO Refresh(TokenModel model) 
        {
            return _tokenService.Refresh(model);
        }
    }
}
