using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
