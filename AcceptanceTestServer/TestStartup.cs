using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcceptanceTestServer
{
    public class TestStartup : WebTest.Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            ReplaceServices(services, GetReplacementServices(services));
        }

        private IEnumerable<ServiceDescriptor> GetReplacementServices(IServiceCollection services)
        {
            // Must be a better way to pass in the replacement services from the host building steps!
            var descriptor = services.FirstOrDefault(o => o.ServiceType == typeof(ReplacementServicesProvider));
            var service = descriptor?.ImplementationInstance as ReplacementServicesProvider;
            if (service == null)
                throw new ArgumentNullException("ReplacementServicesProvider", "ReplacementServicesProvider should be added by calling .UseAlternativeServices");

            var replacementServices = service.Get();
            services.Remove(descriptor);

            return replacementServices;
        }

        private static void ReplaceServices(IServiceCollection services, IEnumerable<ServiceDescriptor> replacementServices)
        {
            foreach (var descriptor in replacementServices)
            {
                services.Replace(descriptor);
            }
        }
    }
}
