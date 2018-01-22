using Flurl;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAppSample.Acceptance.Tests
{
    public abstract class WhenBase
    {
        private readonly GivenBase _given;

        protected WhenBase(GivenBase given)
        {
            _given = given;
        }

        protected abstract string CreateRequestUrl();

        protected virtual Dictionary<string, string> GetCookies() => new Dictionary<string, string>();

        protected virtual Dictionary<string, string> GetHeaders() => new Dictionary<string, string>();

        protected void AddQueryParameterIfNotNull<T>(QueryParamCollection queryParams, string name, T value)
        {
            if (value != null)
                queryParams.Add(name, value);
        }

        private async Task<HttpResponseMessage> GetResponse(string url, Dictionary<string, string> cookieValues, Dictionary<string, string> headerValues)
        {
            using (var httpRequest = CreateGetRequest(url, cookieValues, headerValues))
            {
                using (var httpClient = _given.TestServer.CreateClient())
                {
                    var response = await httpClient.SendAsync(httpRequest);
                    response.EnsureSuccessStatusCode();
                    return response;
                }
            }
        }

        protected async Task<string> GetResponseString(string url, Dictionary<string, string> cookieValues, Dictionary<string, string> headerValues)
        {
            using (var response = await GetResponse(url, cookieValues, headerValues))
                return await response.Content.ReadAsStringAsync();
        }

        protected virtual HttpRequestMessage CreateGetRequest(string url, Dictionary<string, string> cookieValues, Dictionary<string, string> headerValues)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (cookieValues != null)
            {
                foreach (var cookie in cookieValues.Where(kv => kv.Value != null))
                {
                    request.Headers.Add("cookie", $"{cookie.Key}={cookie.Value}");
                }
            }

            if (headerValues != null)
            {
                foreach (var header in headerValues.Where(kv => kv.Value != null))
                    request.Headers.Add(header.Key, header.Value);
            }

            return request;
        }
    }
}
