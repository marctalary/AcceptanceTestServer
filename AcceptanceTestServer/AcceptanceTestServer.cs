using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration.FileExtensions;
using Newtonsoft.Json;
using WebTest;
using Xunit;

namespace AcceptanceTestServer
{
    public class AcceptanceTestServer
    {
        [Fact]
        public void ConfigAndStart()
        {
            TestServer testServer = null;
            IEnumerable enumerableService = null;

            testServer = CreateTestServer(new List<ServiceDescriptor>());
            enumerableService = testServer.GetService<IEnumerable>();
            Assert.IsType<ArrayList>(enumerableService);

            var replacementServices = new List<ServiceDescriptor> { new ServiceDescriptor(typeof(IEnumerable), new SortedList()) };

            testServer = CreateTestServer(replacementServices);
            enumerableService = testServer.GetService<IEnumerable>();
            Assert.IsType<SortedList>(enumerableService);

        }

        public TestServer CreateTestServer(IEnumerable<ServiceDescriptor> replacementServices)
        {
            var contentRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "WebTest"));

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseAlternativeServices(replacementServices)
                .UseStartup<TestStartup>();

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }
    }
}
