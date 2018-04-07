using Autofac;
using Postogram.Common;
using Postogram.Common.Configuration;
using Postogram.Common.Logger;
using Postogram.EfDal;
using Postogram.Server.Configuration;
using Postogram.Server.Logger;

namespace Postogram.Server
{
    public class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new FileHelper()).As<FileHelper>();
            builder.RegisterType<SerilogAdapter>().As<ILogger>();
            builder.RegisterType<AppSettingsConfiguration>().As<IConfiguration>();

            builder.RegisterModule<DbModule>();

            builder.RegisterConfigurationSection<InstagramConfiguration>();
            builder.RegisterType<Example>();
        }
        
    }
}
