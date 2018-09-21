using System;

namespace Postogram.Common.Container
{
    public interface IContainer : IDisposable
    {
        IContainer CreateContainer();
        TAbstraction Resolve<TAbstraction>();
        object Resolve(Type abstractionType);
    }
}
