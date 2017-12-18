using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureServiceReplacements(this IWebHostBuilder webHostBuilder, IServiceCollection replacementServices)
            => webHostBuilder.ConfigureServices(ServiceReplacer.AddServiceReplacer(replacementServices));
    }
}
