using System;

namespace Postogram.Common.Container
{
    public interface IConfigurator
    {
        void Register<TAbstraction>();
        void Register(Type implementationType);

        void Register<TAbstraction, TImplementation>() where TImplementation : TAbstraction;
        void Register(Type abstractionType, Type implementationType);

        void RegisterSingleton<TAbstraction, TImplementation>() where TImplementation : TAbstraction;
        void RegisterSingleton(Type abstractionType, Type implementationType);

        void Register<TAbstraction>(Func<IConfiguratorContext, TAbstraction> fabric);
        void RegisterSingleton<TAbstraction>(Func<IConfiguratorContext, TAbstraction> fabric);

        IContainer Build();
    }
}
