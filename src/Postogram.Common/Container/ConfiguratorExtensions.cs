using System;
using Postogram.Common.Configuration;

namespace Postogram.Common.Container
{
    public static class ConfiguratorExtensions
    {
        public static void RegisterConfigurationSection<TConfigSection>(this IConfigurator configurator)
            where TConfigSection : IConfigurationSection, new()
        {
            configurator.Register(CreateConfigInstance);

            TConfigSection CreateConfigInstance(IConfiguratorContext context)
            {
                var configurationInstance = context.Resolve<IConfiguration>();
                return configurationInstance.GetConfigSection<TConfigSection>();
            }
        }

        public static void RegisterModule<TModule>(this IConfigurator configurator)
            where TModule : IContainerModule, new()
        {
            var module = new TModule();
            module.Configure(configurator);
        }
    }
}
