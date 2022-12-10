using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(UserRefreshToken), ReverseMap = true)]
    public class UserRefreshTokenDto
    {
         public int Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }
         
        public string IpAddress { get; set; }
        public bool IsInvalidated { get; set; }

        public int UserId { get; set; }
         
    }
}
