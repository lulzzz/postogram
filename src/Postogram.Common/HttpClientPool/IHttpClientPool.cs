using System.Net.Http;

namespace Postogram.Common
{
    public interface IHttpClientPool
    {
        HttpClient GetHttpClient();
        HttpClient GetHttpClient(string name);
    }
}
