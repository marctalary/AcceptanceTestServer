using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AcceptanceTestServer.Sample;
using Xunit;

namespace AcceptanceTestServer
{
    public class AcceptanceTestServerTests
    {
        private string ContentRoot => Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "WebTest"));

        [Fact]
        public void ServiceShouldBeConfiguredAsOriginallyConfiguredInWebTest()
        {
            // Given
            var replacementServices = new ServiceCollection();

            // When
            var testServer = AcceptanceTestServer.Create<WebTestStartupWrapper>(replacementServices, ContentRoot);
            var enumerableService = testServer.GetService<IEnumerable>();

            // Then
            Assert.IsType<ArrayList>(enumerableService);
        }

        [Fact]
        public void ServiceShouldBeConfiguredWithReplacement()
        {
            // Given
            var replacementServices = new ServiceCollection { new ServiceDescriptor(typeof(IEnumerable), new SortedList()) };

            // When
            var testServer = AcceptanceTestServer.Create<WebTestStartupWrapper>(replacementServices, ContentRoot);
            var enumerableService = testServer.GetService<IEnumerable>();

            // Then
            Assert.IsType<SortedList>(enumerableService);
        }
    }
}
