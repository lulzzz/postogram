using System;
using System.Linq;
using Akka.Actor;
using Akka.Routing;
using Postogram.InstagramClient.Actors.ChildActors;

namespace Postogram.InstagramClient.Actors
{
    public class InstaPostCoordinatorActor : ReceiveActor, IWithUnboundedStash
    {
        private IActorRef _poster;
        public IStash Stash { get; set; }

        public InstaPostCoordinatorActor()
        {
            Handle();
        }

        protected override void PreStart()
        {
            int postersCount = Environment.ProcessorCount;

            var posters = Enumerable
                .Range(0, postersCount)
                .Select(i => Context.ActorOf<InstaPosterActor>($"poster_{i}"))
                .ToArray();

            var paths = posters
                .Select(poster => poster.Path.ToString())
                .ToArray();
            
            _poster = Context.ActorOf(Props.Empty.WithRouter(new RoundRobinGroup(paths)), "posters");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(_ => Directive.Restart);
        }

        private int counter = 0;

        private void Handle()
        {
            Receive<PostContentMessage>(message =>
            {
                _poster.Tell(message.WithNumber(counter++));
            });

            Receive<PostContentResultSuccess>(message => Console.WriteLine($"{message.TaskNumber} t#{message.ThreadId} Success"));
            Receive<PostContentResultFail>(message => Console.WriteLine($"{message.TaskNumber} t#{message.ThreadId} Fail {message.Reason}"));
        }
    }
}
