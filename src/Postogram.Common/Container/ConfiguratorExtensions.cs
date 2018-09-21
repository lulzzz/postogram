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
    }
}
