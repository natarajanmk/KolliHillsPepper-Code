using KH.Pepper.Core.AppServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.Pepper.Web.Config
{
    public class ApiUserSessionProvider : IUserSessionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiUserSessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<UserSession> GetUserSession(CancellationToken cancellationToken)
        {
            var principal = _httpContextAccessor.HttpContext!.User;

            await Task.CompletedTask;

            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["x-ms-token-aad-access-token"];
            return new UserSession(principal, accessToken);
        }
    }
}
