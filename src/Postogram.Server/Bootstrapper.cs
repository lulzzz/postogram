using System;
using Akka.Actor;
using Postogram.Common.Container;
using Postogram.Common.Container.Akka;
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
            using (var system = ActorSystem.Create("sketch"))
            {
                var resolver = new AkkaDependencyResolver(system, Container);

                var greater = system.ActorOf(resolver.Create<GreaterActor>());
                greater.Tell(new Greeting("test"));

                while (Console.ReadLine() != "q"){}
            }
        }

        private IContainer InitContainer()
        {
            var builder = new AutofacConfigurator();
            builder.RegisterModule<ServerModule>();

            builder.Register<GreaterActor>();

            return builder.Build();
        }
    }
}
