using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{
    [AutoMap(typeof(User), ReverseMap = true)]
    public class UserDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

      
        public string PhoneNumber { get; set; }

         
        public string EmailAddress { get; set; }

         
        public string Password { get; set; }

        public string OneTimePassword { get; set; }

        public Boolean? IsActive { get; set; }
        
        public string Address { get; set; }
 
        public string Location { get; set; }

        public string PinCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
