using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer
{
    public static class AcceptanceTestServer
    {
        public static TestServer Create<TStartup>(IServiceCollection replacementServices, string contentRoot) where TStartup : class
        {
            if (replacementServices == null) throw new ArgumentNullException(nameof(replacementServices));
            if (contentRoot == null) throw new ArgumentNullException(nameof(contentRoot));

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(cs => cs.AddSingleton(new ServiceReplacer(replacementServices)))
                .UseStartup<TStartup>();

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }
    }
}
