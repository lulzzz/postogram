using Postogram.Common.Container;

namespace Postogram.Common
{
    public class DefaultModule : IContainerModule
    {
        public void Configure(IConfigurator configurator)
        {
            configurator.Register<IHttpClientPool, HttpClientPool>();
        }
    }
}
