using System;

namespace Postogram.Common.Container
{
    public interface IConfiguratorContext
    {
        TAbstraction Resolve<TAbstraction>();
        object Resolve(Type abstractionType);
    }
}
