using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer.Sample
{
    public class WebTestStartupWrapper : WebTest.Startup
    {
        public WebTestStartupWrapper(IHostingEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            ServiceReplacer.ReplaceServices(services);
        }
    }
}
