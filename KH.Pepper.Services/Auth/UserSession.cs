using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace KH.Pepper.Core.AppServices
{
    public sealed class UserSession
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }

        public IReadOnlyList<string> Roles { get; } = new List<string>();

        public IReadOnlyList<string> Permissions { get; } = new List<string>();

        public bool HasRole(string role) => Roles.Contains(role);

        public UserSession(string fullName, string accessToken, IReadOnlyList<string> roles, 
            IReadOnlyList<string> permissions)
        {
            UserName = fullName;
            AccessToken = accessToken;
            Roles = roles;
            Permissions = permissions;
        }   

        public UserSession(ClaimsPrincipal principal)
        {
            UserName = principal.Claims.FirstOrDefault()?.Value;
            Roles = principal.Claims.Select(x => x.Value).ToList().AsReadOnly();
            Permissions = Roles.ToPermissions();
        }

        public UserSession(ClaimsPrincipal principal,string accessToken) : this(principal)
        {
            AccessToken = accessToken;
        }

    }
}
