using System;
using Autofac;
using Postogram.Common.Logger;

namespace Postogram.Server
{
    public class Bootstrapper
    {
        public IContainer Container { get; }

        public Bootstrapper()
        {
            Container = InitContainer();
        }

        public void StartApp()
        {
            throw new NotImplementedException();
        }

        private IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServerModule>();

            return builder.Build();
        }
    }
}
