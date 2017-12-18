using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace AcceptanceTestServer
{
    public class ServiceReplacer
    {
        private readonly IServiceCollection _replacementServices;

        public ServiceReplacer(IServiceCollection replacementServices)
        {
            _replacementServices = replacementServices ?? throw new ArgumentNullException(nameof(replacementServices));
        }

        public IServiceCollection Get() => _replacementServices;

        public static void ReplaceServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            ReplaceServices(services, GetReplacementServices(services));
        }
        public static Action<IServiceCollection> AddServiceReplacer(IServiceCollection replacementServices)
        {
            return services => services.AddSingleton(new ServiceReplacer(replacementServices));
        }

        private static IServiceCollection GetReplacementServices(IServiceCollection services)
        {
            // Must be a better way to pass in the replacement services from the host building steps!
            var descriptor = services.FirstOrDefault(o => o.ServiceType == typeof(ServiceReplacer));
            var serviceReplacer = descriptor?.ImplementationInstance as ServiceReplacer;
            if (serviceReplacer == null)
                throw new ArgumentNullException("ServiceReplacer", "Call IWebHostBuilder.ConfigureServices(ServiceReplacer.AddServiceReplacer(replacementServices)) to configure ServiceReplacer");

            var replacementServices = serviceReplacer.Get();
            services.Remove(descriptor);

            return replacementServices;
        }

        private static void ReplaceServices(IServiceCollection services, IServiceCollection replacementServices)
        {
            foreach (var descriptor in replacementServices)
            {
                services.Replace(descriptor);
            }
        }
    }
}
