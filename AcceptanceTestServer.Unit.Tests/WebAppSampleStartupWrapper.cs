using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer.Tests
{
    public class WebAppSampleStartupWrapper : WebAppSample.Startup
    {
        public WebAppSampleStartupWrapper(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            ServiceReplacer.ReplaceServices(services);
        }
    }
}
