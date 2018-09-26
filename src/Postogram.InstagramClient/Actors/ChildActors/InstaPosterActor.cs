using System;
using System.IO;
using System.Threading;
using Akka.Actor;

namespace Postogram.InstagramClient.Actors.ChildActors
{
    public class InstaPosterActor : ReceiveActor
    {
        public InstaPosterActor()
        {
            Handle();
        }

        private void Handle()
        {
            Receive<PostContentMessage>(message =>
            {
                if (message.Number % 666 == 0)
                    throw new ApplicationException("some exception");

                Fibonacci(message.Number);
                var even = message.Number % 2 == 0;
                if (even)
                    Context.Parent.Tell(new PostContentResultSuccess(message.Content, message.Number, Thread.CurrentThread.ManagedThreadId));
                else
                    Context.Parent.Tell(new PostContentResultFail(message.Content, message.Number, Thread.CurrentThread.ManagedThreadId, $"idk {Path.GetRandomFileName()}"));
            });
        }

        public static int Fibonacci(int n)
        {
            int a = 0;
            int b = 1;
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }
}
