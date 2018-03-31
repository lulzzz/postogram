using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postogram.Common;
using Postogram.Server.Configuration;
using Postogram.Server.Logger;

namespace Postogram.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new SerilogAdapter(new FileHelper());
            var writer = logger.CreateWriter<Program>();
            writer.Info("Hello '{World}'", new { World = "Mars" });

            Console.ReadLine();
        }
    }
}
