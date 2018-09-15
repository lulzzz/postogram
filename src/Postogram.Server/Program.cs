using System;

namespace Postogram.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Bootstrapper()
                .StartApp();

            Console.ReadLine();
        }
    }
}
