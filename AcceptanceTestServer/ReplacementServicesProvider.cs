using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AcceptanceTestServer
{
    public class ReplacementServicesProvider
    {
        private readonly IEnumerable<ServiceDescriptor> _replacementServices;

        public ReplacementServicesProvider(IEnumerable<ServiceDescriptor> replacementServices)
        {
            _replacementServices = replacementServices;
        }

        public IEnumerable<ServiceDescriptor> Get() => _replacementServices;
    }
}
