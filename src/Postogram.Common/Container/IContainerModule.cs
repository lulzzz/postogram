using System;

namespace Postogram.Common.Container
{
    public interface IContainerModule
    {
        void Configure(IConfigurator configurator);
    }
}
