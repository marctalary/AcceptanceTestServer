using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;

namespace WebAppSample.Acceptance.Tests
{
    public class GivenBase
    {
        protected GivenBase()
        {
            StartTestServer();
        }

        protected IServiceProvider ServiceProvider { get; private set; }

        public TestServer TestServer { get; private set; }
        public string CurrentSite
        {
            get => TestServer.BaseAddress.ToString();
            set => TestServer.BaseAddress = new Uri(value, UriKind.RelativeOrAbsolute);
        }
        private void StartTestServer()
        {
            var webSourceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", typeof(Startup).Namespace);

            // https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(webSourceDirectory)
                .UseStartup<WebAppSampleStartupWrapper>();

            TestServer = new TestServer(builder)
            {
                BaseAddress = new Uri("https://mywebsite.com", UriKind.Absolute)

            };

            ServiceProvider = TestServer.Host.Services;
        }
    }
}
