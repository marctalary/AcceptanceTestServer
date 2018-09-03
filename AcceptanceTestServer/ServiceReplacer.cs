using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AcceptanceTestServer
{
    public class ServiceReplacer
    {
        private readonly IServiceCollection _replacementServices;

        public ServiceReplacer(IServiceCollection replacementServices)
        {
            _replacementServices = replacementServices ?? throw new ArgumentNullException(nameof(replacementServices));
        }

        public void ReplaceServices(IServiceCollection services)
        {
            foreach (var replacementService in _replacementServices)
                services.Replace(replacementService);
        }
    }
}
