using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTestServer
{
    public static class TestServerExtensions
    {
        public static T GetService<T>(this TestServer testServer) 
        {
            return testServer.Host.Services.GetService<T>();
        }
    }
}
