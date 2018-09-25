using System;
using Autofac;
using Postogram.Common.Container;
using IContainer = Postogram.Common.Container.IContainer;

namespace Postogram.Server.Container
{
    public class AutofacConfigurator : IConfigurator
    {
        readonly ContainerBuilder _containerBuilder;

        public AutofacConfigurator()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public AutofacConfigurator(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));
        }

        public IContainer Build()
        {
            return new AutofacContainer(_containerBuilder.Build());
        }


        //Per request
        public void Register<TImplementation>()
        {
            Register(typeof(TImplementation));
        }

        public void Register(Type implementationType)
        {
            _containerBuilder.RegisterType(implementationType);
        }

        public void Register<TAbstraction, TImplementation>()
            where TImplementation : TAbstraction
        {
            Register(typeof(TAbstraction), typeof(TImplementation));
        }

        public void Register(Type abstractionType, Type implementationType)
        {
            _containerBuilder
                .RegisterType(implementationType)
                .As(abstractionType);
        }


        //Singleton
        public void RegisterSingleton<TAbstraction, TImplementation>()
            where TImplementation : TAbstraction
        {
            RegisterSingleton(typeof(TAbstraction), typeof(TImplementation));
        }

        public void RegisterSingleton(Type abstractionType, Type implementationType)
        {
            _containerBuilder
                .RegisterType(implementationType)
                .As(abstractionType)
                .SingleInstance();
        }


        //With fabric
        public void Register<TAbstraction>(Func<IConfiguratorContext, TAbstraction> fabric)
        {
            _containerBuilder
                .Register<TAbstraction>(ctx => fabric(new AutofacConfiguratorContext(ctx)))
                .InstancePerLifetimeScope();
        }
        
        public void RegisterSingleton<TAbstraction>(Func<IConfiguratorContext, TAbstraction> fabric)
        {
            _containerBuilder
                .Register<TAbstraction>(ctx => fabric(new AutofacConfiguratorContext(ctx)))
                .SingleInstance();
        }
    }
}
