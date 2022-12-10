using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class UserRefreshTokenRepository : BaseRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
