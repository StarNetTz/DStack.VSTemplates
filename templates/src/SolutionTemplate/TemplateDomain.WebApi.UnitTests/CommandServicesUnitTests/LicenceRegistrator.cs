using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace $safeprojectname$
{
    public static class LicenceRegistrator
    {
        public static void RegisterServiceStackLicenceWhenNrOfServicesExceedesFreeThreshold()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            Licensing.RegisterLicense(configuration["ServiceStack:Licence"]);
        }
    }
}