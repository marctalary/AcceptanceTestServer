using AcceptanceTestServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppSample.Acceptance.Tests
{
    public class WebAppSampleStartupWrapper : Startup
    {
        public WebAppSampleStartupWrapper(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.ReplaceServices();
        }
    }
}
