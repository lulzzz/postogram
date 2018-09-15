using Autofac;
using Postogram.Common;
using Postogram.Common.Configuration;
using Postogram.Common.Logger;
using Postogram.EfDal;
using Postogram.Server.Logger;
using Postogram.Server.Configuration;

namespace Postogram.Server
{
    public class ServerModule : Module
    {
        //Uses DbModule from EfDal
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilePathHelper>().As<IFilePathHelper>().SingleInstance();
            builder.RegisterType<SerilogAdapter>().As<ILogger>();

            builder.RegisterType<AppSettingsConfiguration>().As<IConfiguration>();
            RegisterConfigurations(builder);

            builder.RegisterModule<DbModule>();
        }

        private void RegisterConfigurations(ContainerBuilder builder)
        {
            builder.RegisterConfigurationSection<ApplicationEnviromentConfiguration>();
            builder.RegisterConfigurationSection<CustomPathsConfiguration>();
        }
    }
}
