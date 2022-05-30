using ServiceStack.Auth;
using ServiceStack;

[assembly: HostingStartup(typeof(TemplateDomain.WebApi.ConfigureAuth))]

namespace TemplateDomain.WebApi
{
    public class ConfigureAuth : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureAppHost(appHost => {
                var appSettings = appHost.AppSettings;
                var x = appSettings.GetString("jwt:PublicKeyXML");
                appHost.Plugins.Add(
                    new AuthFeature(() => new AuthUserSession(),
                    new IAuthProvider[] {
                       new JwtAuthProviderReader(appSettings) {
                            RequireSecureConnection = Convert.ToBoolean( appSettings.GetString("jwt:RequireSecureConnection")),
                            EncryptPayload = Convert.ToBoolean( appSettings.GetString("jwt:EncryptPayload")),
                            PublicKeyXml = appSettings.GetString("jwt:PublicKeyXML"),
                            HashAlgorithm = "RS256"
                        }
                    }));
            });
    }
}