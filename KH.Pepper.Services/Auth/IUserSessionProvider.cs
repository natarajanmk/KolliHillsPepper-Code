using System.Threading;
using System.Threading.Tasks;

namespace KH.Pepper.Core.AppServices
{
    public interface IUserSessionProvider
    {
        Task<UserSession> GetUserSession(CancellationToken cancellationToken);
    }
}
