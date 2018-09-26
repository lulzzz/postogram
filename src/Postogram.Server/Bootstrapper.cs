using System;
using Akka.Actor;
using Postogram.Common.Container;
using Postogram.Common.Container.Akka;
using Postogram.InstagramClient.Actors;
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

                var instagramPoster = system.ActorOf<InstaPostCoordinatorActor>();
                while (Console.ReadLine() != "q")
                {
                    for(int i = 0; i < 10_000; i++)
                        instagramPoster.Tell(new PostContentMessage(null, null, i));
                }
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
