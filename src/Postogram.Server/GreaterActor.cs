using System;
using Akka.Actor;
using Postogram.Common.Configuration;

namespace Postogram.Server
{
    public class GreaterActor : UntypedActor
    {
        private readonly ApplicationEnviromentConfiguration _configuration;

        public GreaterActor(ApplicationEnviromentConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case Greeting greeting:
                    Console.WriteLine($"Hello {greeting.Text} from {_configuration.ApplicationName}");
                    break;
            }
        }
    }

    public class Greeting
    {
        public Greeting(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
