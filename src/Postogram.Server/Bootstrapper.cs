using System;
using Postogram.Common.Container;
using Postogram.Server.Container;

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
            var builder = new AutofacConfigurator();
            builder.RegisterModule<ServerModule>();
            return builder.Build();
        }
    }
}
