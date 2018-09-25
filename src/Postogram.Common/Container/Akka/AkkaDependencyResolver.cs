using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Akka.Actor;
using Akka.DI.Core;

namespace Postogram.Common.Container.Akka
{
    public class AkkaDependencyResolver  : IDependencyResolver, INoSerializationVerificationNeeded
    {
        private readonly ActorSystem _system;
        private readonly IContainer _container;
        private readonly ConcurrentDictionary<string, Type> _knownActorTypes;
        private readonly ConditionalWeakTable<ActorBase, IContainer> _containers;

        public  AkkaDependencyResolver(ActorSystem system, IContainer container)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
            _container = container ?? throw new ArgumentNullException(nameof(container));

            _containers = new ConditionalWeakTable<ActorBase, IContainer>();
            _knownActorTypes = new ConcurrentDictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

            system.AddDependencyResolver(this);
        }

        public Type GetType(string actorName)
        {
            return _knownActorTypes.GetOrAdd(actorName, _ => new Lazy<Type>(FindType).Value);

            Type FindType()
            {
                return actorName.GetTypeValue();
            }
        }

        public Props Create<TActor>() where TActor : ActorBase => Create(typeof(TActor));
        public Props Create(Type actorType)
        {
            return _system.GetExtension<DIExt>().Props(actorType);
        }

        public Func<ActorBase> CreateActorFactory(Type actorType)
        {
            return CreateActor;

            ActorBase CreateActor()
            {
                var container = _container.CreateContainer();
                var instance = (ActorBase)_container.Resolve(actorType);

                _containers.Add(instance, container);

                return instance;
            }
        }

        public void Release(ActorBase actor)
        {
            if (_containers.TryGetValue(actor, out var container))
            {
                _containers.Remove(actor);
                container.Dispose();
            }
        }
    }
}
