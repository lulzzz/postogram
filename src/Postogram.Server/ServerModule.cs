using Postogram.Common;
using Postogram.Common.Logger;
using Postogram.Common.Container;
using Postogram.Common.Configuration;
using Postogram.EfDal;
using Postogram.Server.Logger;
using Postogram.Server.Configuration;

namespace Postogram.Server
{
    public class ServerModule : IContainerModule
    {
        public void Configure(IConfigurator configurator)
        {
            configurator.RegisterSingleton<IFilePathHelper, FilePathHelper>();
            configurator.Register<ILogger, SerilogAdapter>();
            configurator.Register<IConfiguration, AppSettingsConfiguration>();
            RegisterConfigurationSections(configurator);

            configurator.RegisterModule<DbModule>();
        }

        private void RegisterConfigurationSections(IConfigurator configurator)
        {
            configurator.Register<CustomPathsConfiguration>();
            configurator.Register<ApplicationEnviromentConfiguration>();
        }
    }
}
