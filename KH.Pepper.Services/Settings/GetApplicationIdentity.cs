
using MediatR;
using System.Reflection;

namespace KH.Pepper.Core.AppServices
{
    public class GetApplicationIdentity
    {

        public class Request : IRequest<Response>
        { }

        public class Response
        {
            public ApplicationInfo ApplicationInfo { get; set; }
            public UserInfo UserInfo { get; set; }
        }

        public class UserInfo
        {
            public string UserName { get; set; }
            public List<string> Roles { get; set; }

            public List<string> Permissions { get; set; }
        }
        public class ApplicationInfo
        {
            public string Pillar { get; set; }
            public string System { get; set; }
            public string Version{ get; set; }
            public string ApplicationInsightsConnectionString { get; set; }
        }

        public class Handler : IRequestHandler<Request,Response>
        {
            private readonly ApplicationConfiguration _applicationConfiguration;
            private readonly IUserSessionProvider _userSessionProvider;

            public Handler(ApplicationConfiguration applicationConfiguration, IUserSessionProvider userSessionProvider)
            {
                _applicationConfiguration = applicationConfiguration;
                _userSessionProvider = userSessionProvider;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userSession = await _userSessionProvider.GetUserSession(cancellationToken);

                return new Response
                {
                    UserInfo = new UserInfo
                    {
                        UserName = userSession.UserName,
                        Roles = userSession.Roles.ToList(),
                        Permissions = userSession.Permissions.ToList(),
                    },
                    ApplicationInfo = new ApplicationInfo
                    {
                        Pillar = _applicationConfiguration.Pillar ?? "localhost",
                        System = _applicationConfiguration.System ?? "O",
                        Version = GetVersion().ToString(),
                        ApplicationInsightsConnectionString = _applicationConfiguration.ApplicationInsightsConnectionString
                    }
                };
            }
        }

        private static Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
