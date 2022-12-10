using System.Collections.Generic;

namespace KH.Pepper.Core.AppServices
{
    public static class RoleToPermissions
    {
        private static readonly Dictionary<string, List<string>> Mapping = new Dictionary<string, List<string>>();

        public static IReadOnlyList<string> ToPermissions(this IReadOnlyList<string> roles)
        {
            var permissions = new List<string>();

            foreach(var role in roles)
            {
                permissions.AddRange(GetPermissions(role));
            }
            return permissions;
        }

        private static IReadOnlyList<string> GetPermissions(string role)
        {
            if (Mapping.ContainsKey(role))
            {
                return Mapping[role];
            }
            return new List<string>();
        }

    }
}
