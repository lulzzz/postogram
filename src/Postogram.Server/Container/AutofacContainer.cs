using System;
using Autofac;
using IContainer = Postogram.Common.Container.IContainer;

namespace Postogram.Server.Container
{
    public class AutofacContainer : IContainer
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacContainer(Autofac.IContainer container)
        {
            _lifetimeScope = container.BeginLifetimeScope();
        }

        public AutofacContainer(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Dispose() => _lifetimeScope.Dispose();
        public IContainer CreateContainer() => new AutofacContainer(_lifetimeScope.BeginLifetimeScope());

        public object Resolve(Type abstractionType) => _lifetimeScope.Resolve(abstractionType);
        public TAbstraction Resolve<TAbstraction>() => _lifetimeScope.Resolve<TAbstraction>();
    }
}
