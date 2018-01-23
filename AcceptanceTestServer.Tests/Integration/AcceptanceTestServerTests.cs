using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections;
using System.IO;
using Xunit;

namespace AcceptanceTestServer.Tests.Integration
{
    public class AcceptanceTestServerTests
    {
        // Assumes the 
        private string ContentRoot => Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", typeof(WebAppSample.Startup).Namespace));

        [Fact]
        public void Create_ServiceShouldBeConfiguredAsOriginallyConfiguredInWebTest()
        {
            // Given
            var replacementServices = new ServiceCollection();

            // When
            var testServer = AcceptanceTestServer.Create<WebAppSampleStartupWrapper>(replacementServices, ContentRoot);
            var enumerableService = testServer.GetService<IEnumerable>();

            // Then
            Assert.IsType<ArrayList>(enumerableService);
        }

        [Fact]
        public void Create_ServiceShouldBeConfiguredWithReplacement()
        {
            // Given
            var replacementServices = new ServiceCollection { new ServiceDescriptor(typeof(IEnumerable), new SortedList()) };

            // When
            var testServer = AcceptanceTestServer.Create<WebAppSampleStartupWrapper>(replacementServices, ContentRoot);
            var enumerableService = testServer.GetService<IEnumerable>();

            // Then
            Assert.IsType<SortedList>(enumerableService);
        }
    }
}
