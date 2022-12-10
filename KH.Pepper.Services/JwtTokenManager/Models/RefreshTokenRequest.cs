using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.AppServices.JwtTokenManager
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
