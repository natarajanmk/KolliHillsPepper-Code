﻿using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.JwtTokenManager
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
