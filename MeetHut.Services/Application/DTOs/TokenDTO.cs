﻿using System;

namespace MeetHut.Services.Application.DTOs
{
    /// <summary>
    /// Login Response
    /// </summary>
    public class TokenDTO
    {
        /// <summary>
        /// Access Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Refresh token expires in
        /// </summary>
        public DateTime RefreshTokenExpiresIn { get; set; }
    }
}
