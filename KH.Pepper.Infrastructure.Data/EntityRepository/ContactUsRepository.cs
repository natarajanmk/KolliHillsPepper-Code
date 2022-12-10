using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class ContactUsRepository : BaseRepository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
