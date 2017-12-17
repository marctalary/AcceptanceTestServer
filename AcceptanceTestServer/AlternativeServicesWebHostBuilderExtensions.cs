using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AcceptanceTestServer
{
    public static class AlternativeServicesWebHostBuilderExtensions
    {
        public static IWebHostBuilder UseAlternativeServices(this IWebHostBuilder hostBuilder, IEnumerable<ServiceDescriptor> replacementServices)
        {
            hostBuilder.ConfigureServices(services => services.AddSingleton(new ReplacementServicesProvider(replacementServices)));
            return hostBuilder;
        }
    }
}