using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postogram.InstagramClient;
using Postogram.Server.Configuration;

namespace Postogram.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var class1 = new Class1(new AppSettingsConfiguration());
            Console.ReadLine();
        }
    }
}
