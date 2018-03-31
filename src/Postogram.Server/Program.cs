using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Postogram.Common;
using Postogram.Common.Configuration;
using Postogram.Common.Logger;
using Postogram.Server.Configuration;
using Postogram.Server.Logger;

namespace Postogram.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ServerModule>();
            var container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);

            var ex = container.Resolve<Example>();

            Console.ReadLine();
        }
    }

    public class Example
    {
        public Example(InstagramConfiguration section, ILogger logger)
        {
            var writer = logger.CreateWriter<Example>();
            writer.Info("Login {login} password {password}", section.Login, section.Password);
        }
    }

    public class InstagramConfiguration : IConfigurationSection
    {
        public void Init(IConfigurationReader reader)
        {
            Login = reader.Read<string>(nameof(Login));
            Password = reader.Read<string>(nameof(Password));
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
    }
}
