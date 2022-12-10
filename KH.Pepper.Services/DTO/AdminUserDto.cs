using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(AdminUser), ReverseMap = true)]
    public class AdminUserDto
    {
        public int Id { get; set; }

         
        public string UserName { get; set; }
 
        public string Password { get; set; }

        public Boolean? IsActive { get; set; }
    }
}
