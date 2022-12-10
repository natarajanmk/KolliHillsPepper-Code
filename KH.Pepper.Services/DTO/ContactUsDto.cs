using AutoMapper;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Core.AppServices.Dto
{

    [AutoMap(typeof(ContactUs), ReverseMap = true)]
    public class ContactUsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Descriptions { get; set; }
    }
}
