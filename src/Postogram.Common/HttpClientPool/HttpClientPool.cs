using System;
using System.Net.Http;
using System.Collections.Concurrent;

namespace Postogram.Common
{
    public class HttpClientPool : IHttpClientPool
    {
        private readonly ConcurrentDictionary<string, HttpClient> _clients;

        public HttpClientPool()
        {
            _clients = new ConcurrentDictionary<string, HttpClient>(StringComparer.InvariantCultureIgnoreCase);
        }

        public HttpClient GetHttpClient() => GetHttpClient(String.Empty);

        public HttpClient GetHttpClient(string name)
        {
            return _clients.GetOrAdd(name,
                _ => new Lazy<HttpClient>(() => new HttpClient()).Value);
        }
    }
}
