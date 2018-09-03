using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer
{
    public static class ServiceCollectionExtensions
    {
        public static void ReplaceServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var serviceReplacer = serviceProvider.GetService<ServiceReplacer>();
            if (serviceReplacer == null)
                throw new MissingServiceReplacerException();

            serviceReplacer.ReplaceServices(services);
        }
    }
}
